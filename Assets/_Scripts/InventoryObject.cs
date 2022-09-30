using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { Deg0, Deg90, Deg180, Deg270 }

public class InventoryObject : MonoBehaviour
{
    public Orientation currentOrientation = Orientation.Deg0;

    private Transform spriteTransform;
    public Vector2Int objectDimensions;
    //public bool[] objectMatrix;    

    public Vector2Int bagPositionIndices;

    private bool placed = false;
    private Bag associatedBag = null;

    private SnapSprite spriteSnapper;

    private void Awake()
    {
        
    }

    public void ChangeBag(Bag newBag)
    {
        if (this.objectDimensions.x > newBag.bindedGrid.gridDimensions.x)
        {
            this.ForceDefaultOrientation();
        }

        this.associatedBag = newBag;   
    }

    private void Start()
    {
        this.spriteTransform = this.transform.GetChild(0);

        if (this.associatedBag == null)
        {
            this.associatedBag = GameObject.Find("DropBag").GetComponent<Bag>();
            this.bagPositionIndices = Vector2Int.zero;
        }

        this.spriteSnapper = GetComponentInChildren<SnapSprite>();
    }

    public void PickUp()
    {
        this.placed = false;
    }

    private void ForceDefaultOrientation()
    {
        while (this.currentOrientation != Orientation.Deg0)
        {
            this.Rotate(true);
        }
    }

    public void Rotate(bool clockwise)
    {
        if (this.objectDimensions.y > this.associatedBag.bindedGrid.gridDimensions.x)
        {
            //Play invalid sound
            return;
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
        bool shouldFlipX = false;
        bool shouldFlipY = false;

        switch (this.currentOrientation)
        {
            case Orientation.Deg0:                
                break;
            case Orientation.Deg90:                
                finalRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                break;
            case Orientation.Deg180:
                shouldFlipY = true;
                break;
            case Orientation.Deg270:
                finalRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                shouldFlipY = true;
                break;
        }

        this.spriteTransform.rotation = finalRotation;
        
        SpriteRenderer objectSpriteRenderer = this.spriteTransform.gameObject.GetComponent<SpriteRenderer>();
        objectSpriteRenderer.flipX = shouldFlipX;
        objectSpriteRenderer.flipY = shouldFlipY;

        this.spriteSnapper.Snap(this.currentOrientation);

        //Come back to this if you want to try to support concave objects
        /*
        bool[] rotatedObjectMatrix = new bool[this.objectMatrix.Length];

        for (int i = 0; i < oldObjectDimensions.y; i++)
        {
            for (int j = 0; j < oldObjectDimensions.x; j++)
            {
                int numColumns = i % this.objectDimensions.y;

                rotatedObjectMatrix[(i * oldObjectDimensions.x) + j] = this.objectMatrix[(j * oldObjectDimensions.y) + i];                
            }
        }

        this.objectMatrix = rotatedObjectMatrix;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
