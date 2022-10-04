using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { Deg0, Deg90, Deg180, Deg270 }

public class InventoryObject : MonoBehaviour
{
    [SerializeField]
    private string objectName;

    private Orientation currentOrientation = Orientation.Deg0;

    private Transform spriteTransform;

    public bool placed = false;
    private Bag associatedBag = null;

    private SnapSprite spriteSnapper;

    private SpriteRenderer objectSprite;

    private ObjectHighlightObserver highlighter;

    [HideInInspector]
    public Vector2Int objectDimensions;

    [HideInInspector]
    public Vector2Int bagPositionIndices = Vector2Int.zero;

    public bool[,] objectMatrix;

    private void Start()
    {
        this.objectSprite = GetComponentInChildren<SpriteRenderer>();
        this.highlighter = GetComponentInChildren<ObjectHighlightObserver>();

        this.spriteTransform = this.transform.GetChild(0);

        this.spriteSnapper = GetComponentInChildren<SnapSprite>();

        if (this.associatedBag == null)
        {            
            this.LoadObjectDetails();
            this.spriteSnapper.Snap(Orientation.Deg0);
            this.highlighter.SetupHighlightTiles(this.objectMatrix);

            this.associatedBag = GameObject.Find("DropBag").GetComponent<Bag>();
            this.bagPositionIndices = Vector2Int.zero;
            this.gameObject.transform.position = new Vector3(this.associatedBag.bindedGrid.startingPosition.x,
                                                             this.associatedBag.bindedGrid.startingPosition.y,
                                                             -0.15f);
            this.PlaceObjectInBag(this.associatedBag.bindedGrid.startingPosition);
        }
    }

    private void LoadObjectDetails()
    {        
        InventoryObjectDetails objectDetails = InventoryObjectDetailsDict.dict[this.objectName];
        
        this.objectSprite.sprite = objectDetails.objectSprite;
        this.objectDimensions = objectDetails.objectDimensions;
        this.objectMatrix = objectDetails.originalObjectMatrix;
    }

    public void ChangeBag(Bag newBag)
    {
        if (this.objectDimensions.x > newBag.bindedGrid.gridDimensions.x)
        {
            this.ForceDefaultOrientation();
        }

        this.associatedBag = newBag;
    }

    public void PickUpObject(Vector3 objectPosition)
    {
        this.associatedBag.RemoveFromBag(this);
        this.placed = false;
        this.gameObject.transform.position = objectPosition;

        this.RefreshHighlightTiles();
    }

    public void PlaceObjectInBag(Vector3 objectPosition)
    {
        this.associatedBag.PlaceInBag(this);

        this.placed = true;

        this.gameObject.transform.position = new Vector3(objectPosition.x, objectPosition.y, -0.15f);

        this.HighlightObject();
    }

    public void MoveObject(Vector3 objectPosition, Vector2Int bagIndices)
    {
        this.gameObject.transform.position = objectPosition;
        this.bagPositionIndices = bagIndices;

        this.RefreshHighlightTiles();
    }

    public void RefreshHighlightTiles()
    {
        if (this.placed == true)
        {
            return;
        }

        for (int i = 0; i < this.objectDimensions.y; i++)
        {
            for (int j = 0; j < this.objectDimensions.x; j++)
            {
                Vector2Int currentObjectTile = new Vector2Int(j, i);
                Vector2Int targetBagTile = new Vector2Int(this.bagPositionIndices.x + j, this.bagPositionIndices.y + i);
                if (this.associatedBag.GetContentAtIndex(targetBagTile) == null)
                {
                    this.highlighter.NotifyStateChange(currentObjectTile, ObjectState.Valid);
                }
                else
                {
                    this.highlighter.NotifyStateChange(currentObjectTile, ObjectState.Invalid);
                }
            }
        }
    }

    private void ForceDefaultOrientation()
    {
        while (this.currentOrientation != Orientation.Deg0)
        {
            this.Rotate(true);
        }
    }

    private Vector2Int ClampAfterRotation()
    {
        int xOffset = 0;
        int yOffset = 0;

        if ((this.bagPositionIndices.x + this.objectDimensions.x) > this.associatedBag.bindedGrid.gridDimensions.x)
        {
            xOffset = this.associatedBag.bindedGrid.gridDimensions.x - (this.bagPositionIndices.x + this.objectDimensions.x);
        }
        if ((this.bagPositionIndices.y + this.objectDimensions.y) > this.associatedBag.bindedGrid.gridDimensions.y)
        { 
            yOffset = (this.bagPositionIndices.y + this.objectDimensions.y) - this.associatedBag.bindedGrid.gridDimensions.y;
        }

        return new Vector2Int(xOffset, yOffset);
    }

    public Vector2Int Rotate(bool clockwise)
    {
        if (this.objectDimensions.y > this.associatedBag.bindedGrid.gridDimensions.x)
        {
            //Play invalid sound
            return Vector2Int.zero;
        }
        
        Vector2Int oldObjectDimensions = this.objectDimensions;

        this.objectDimensions = new Vector2Int(oldObjectDimensions.y, oldObjectDimensions.x);

        if (clockwise == true)
        {
            this.currentOrientation = (Orientation)((int)(this.currentOrientation + 1) % 4);
        }
        else if (this.currentOrientation != Orientation.Deg0)
        {
            this.currentOrientation -= 1;
        }
        else
        {
            this.currentOrientation = Orientation.Deg270;
        }

        Quaternion finalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        bool shouldFlipY = false;

        switch (this.currentOrientation)
        {
            case Orientation.Deg0:                
                break;
            case Orientation.Deg90:                
                finalRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                shouldFlipY = true;
                break;
            case Orientation.Deg180:
                shouldFlipY = true;
                break;
            case Orientation.Deg270:
                finalRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                
                break;
        }

        this.spriteTransform.rotation = finalRotation;
                
        this.objectSprite.flipY = shouldFlipY;

        this.spriteSnapper.Snap(this.currentOrientation);

        this.RotateObjectMatrix(clockwise, oldObjectDimensions);

        this.highlighter.NotifyRotation(this.objectMatrix);

        Vector2Int offset = this.ClampAfterRotation();

        return offset;
    }

    private void RotateObjectMatrix(bool clockwise, Vector2Int oldObjectDimensions)
    {
        bool[,] transposedMatrix = new bool[oldObjectDimensions.y, oldObjectDimensions.x];

        int width = this.objectMatrix.GetLength(0);
        int height = this.objectMatrix.GetLength(1);

        if (clockwise == true)
        {            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0, j_back = height - 1; j < height; j++, j_back--)
                {
                    transposedMatrix[j, i] = this.objectMatrix[i, j_back];
                }
            }
        }
        else
        {
            for (int i = 0, i_back = width - 1; i < width ; i++, i_back--)
            {
                for (int j = 0; j < height; j++)
                {
                    transposedMatrix[j, i] = this.objectMatrix[i_back, j];
                }
            }
        }

        this.objectMatrix = transposedMatrix;
    }

    public void HighlightObject()
    {
        for (int i = 0; i < this.objectDimensions.y; i++)
        {
            for (int j = 0; j < this.objectDimensions.x; j++)
            {
                Vector2Int index = new Vector2Int(j, i);
                this.highlighter.NotifyStateChange(index, ObjectState.Highlighted);
            }
        }
    }

    public void UnHighlightObject()
    {
        for (int i = 0; i < this.objectDimensions.y; i++)
        {
            for (int j = 0; j < this.objectDimensions.x; j++)
            {
                Vector2Int index = new Vector2Int(j, i);
                this.highlighter.NotifyStateChange(index, ObjectState.Placed);
            }
        }
    }

    public bool IsValidDropSpot()
    {
        for (int i = 0; i < this.objectDimensions.y; i++)
        {
            for (int j = 0; j < this.objectDimensions.x; j++)
            {
                Vector2Int targetBagTile = new Vector2Int(this.bagPositionIndices.x + j, this.bagPositionIndices.y + i);
                if (this.associatedBag.GetContentAtIndex(targetBagTile) != null)
                {
                    return false;
                }               
            }
        }

        return true;
    }

    private void PrintObjectContents()
    {
        string debugString = this.gameObject.name + " Contents:\n";

        for (int i = 0; i < this.objectDimensions.y; i++)
        {
            debugString += "[";
            for (int j = 0; j < this.objectDimensions.x; j++)
            {
                debugString += (this.objectMatrix[j, i] == true) ? " 1 " : " 0 ";
            }
            debugString += "]\n";
        }

        Debug.LogError(debugString);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            this.PrintObjectContents();
        }
    }
}
