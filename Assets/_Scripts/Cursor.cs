using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private SpriteRenderer cursorSprite;
    
    private Transform cursorTransform;

    private Bag[] gameBags;

    private Vector2Int currentPositionInBag = Vector2Int.zero;

    private int currentAssociatedBag = 0;

    private InventoryObject grabbedObject;

    private InventoryObject highlightedObject;

    private float grabOffset = 0.15f;

    private IEnumerator Start()
    {
        this.cursorTransform = GetComponent<Transform>();
        this.cursorSprite = GetComponentInChildren<SpriteRenderer>();
        this.gameBags = this.gameObject.transform.parent.GetComponentsInChildren<Bag>();
        this.cursorTransform.position = new Vector3(this.gameBags[0].bindedGrid.startingPosition.x,
                                                    this.gameBags[0].bindedGrid.startingPosition.y, -0.2f);

        yield return null;

        this.HighlightPotentialObject();
    }

    private void MoveRight()
    {
        if (this.grabbedObject != null && 
            (this.currentPositionInBag.x + this.grabbedObject.objectDimensions.x) >=
            this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x)
        {
            if (this.currentAssociatedBag == 0)
            {
                this.currentAssociatedBag++;
                this.currentPositionInBag = new Vector2Int(0, this.currentPositionInBag.y);
                this.grabbedObject.ChangeBag(this.gameBags[this.currentAssociatedBag]);
                MoveCursor();
                return;
            }
            else
            {
                //Sound for invalid move
                return;
            }
        }

        int targetIndex = this.currentPositionInBag.x + 1;
        if (this.highlightedObject != null && this.grabbedObject == null)
        {
            targetIndex += this.highlightedObject.objectDimensions.x - 1;
        }

        if (targetIndex < this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x)
        {
            this.currentPositionInBag = new Vector2Int(targetIndex, this.currentPositionInBag.y);
        }
        else if (this.currentAssociatedBag < (this.gameBags.Length - 1))
        {
            this.currentAssociatedBag++;
            this.currentPositionInBag = new Vector2Int(0, this.currentPositionInBag.y);

            if (this.grabbedObject != null)
            {
                this.grabbedObject.ChangeBag(this.gameBags[this.currentAssociatedBag]);
            }
        }
        else
        { 
            //Sound for invalid move
        }

        this.MoveCursor();
    }

    private void MoveLeft()
    {
        if (this.grabbedObject != null && this.grabbedObject.objectDimensions.x > 1)
        {
            if (this.currentAssociatedBag != 0)
            {
                this.currentAssociatedBag--;

                int xIndex = 0;
                int yIndex = this.currentPositionInBag.y;

                if (this.grabbedObject.objectDimensions.x > this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x)
                {
                    RotateObject(true);
                }

                if ((this.grabbedObject.objectDimensions.y + yIndex) > this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.y)
                {
                    yIndex = (this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.y - this.grabbedObject.objectDimensions.x);
                }

                if (this.grabbedObject.objectDimensions.x > 0)
                {
                    xIndex = (this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x - this.grabbedObject.objectDimensions.x);
                }                

                this.currentPositionInBag = new Vector2Int(xIndex, yIndex);
                this.grabbedObject.ChangeBag(this.gameBags[this.currentAssociatedBag]);
                MoveCursor();
                return;
            }
            else
            {
                //Sound for invalid move
                return;
            }
        }

        int targetIndex = this.currentPositionInBag.x - 1;

        if (targetIndex >= 0)
        {
            this.currentPositionInBag = new Vector2Int(targetIndex, this.currentPositionInBag.y);
        }
        else if (this.currentAssociatedBag > 0)
        {
            this.currentAssociatedBag--;
            this.currentPositionInBag = new Vector2Int(this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x - 1,
                                                        this.currentPositionInBag.y);

            if (this.grabbedObject != null)
            {
                this.grabbedObject.ChangeBag(this.gameBags[this.currentAssociatedBag]);
            }
        }
        else
        {
            //Sound for invalid move
        }

        this.MoveCursor();
    }

    private void MoveUp()
    {
        int targetIndex = this.currentPositionInBag.y - 1;

        if (targetIndex >= 0)
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x, targetIndex);
        }
        else
        { 
            //Sound for invalid move
        }

        this.MoveCursor();
    }

    private void MoveDown()
    {
        if (this.grabbedObject != null &&
            (this.currentPositionInBag.y + this.grabbedObject.objectDimensions.y) >=
            this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.y)
        {
            //Sound for invalid move
            return;
        }

        int targetIndex = this.currentPositionInBag.y + 1;
        if (this.highlightedObject != null && this.grabbedObject == null)
        {
            targetIndex += this.highlightedObject.objectDimensions.y - 1;
        }

        if (targetIndex < this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.y)
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x, targetIndex);
        }
        else
        {
            //Sound for invalid move
        }

        this.MoveCursor();
    }

    private void MoveCursor()
    {
        Vector3 newPosition = new Vector3(this.gameBags[this.currentAssociatedBag].bindedGrid.startingPosition.x + this.currentPositionInBag.x,
                                          this.gameBags[this.currentAssociatedBag].bindedGrid.startingPosition.y - this.currentPositionInBag.y,
                                          this.cursorTransform.position.z);

        this.cursorTransform.position = newPosition;

        if (this.grabbedObject != null)
        {
            Vector3 newObjectPosition = new Vector3(newPosition.x + this.grabOffset, newPosition.y + grabOffset, -0.15f);
            this.grabbedObject.MoveObject(newObjectPosition, this.currentPositionInBag);
        }
        else
        {
            this.HighlightPotentialObject();
        }
    }

    private void AdjustCursorToHighlightedObject(Vector2Int targetIndices)
    {
        if (this.grabbedObject != null)
        {
            return;
        }

        this.currentPositionInBag = targetIndices;

        Vector3 newPosition = new Vector3(this.gameBags[this.currentAssociatedBag].bindedGrid.startingPosition.x + this.currentPositionInBag.x,
                                              this.gameBags[this.currentAssociatedBag].bindedGrid.startingPosition.y - this.currentPositionInBag.y,
                                              this.cursorTransform.position.z);

        this.cursorTransform.position = newPosition;
    }

    private void HighlightPotentialObject()
    {
        InventoryObject potentialHighlightedObject = this.gameBags[this.currentAssociatedBag].GetContentAtIndex(this.currentPositionInBag);

        if (potentialHighlightedObject == null)
        {
            if (this.highlightedObject != null)
            {
                this.highlightedObject.UnHighlightObject();
                this.highlightedObject = null;
            }
        }
        else if (potentialHighlightedObject.placed == false)
        {
            return;
        }
        else if (this.highlightedObject == null)
        {
            this.highlightedObject = potentialHighlightedObject;
            this.highlightedObject.HighlightObject();
            this.AdjustCursorToHighlightedObject(this.highlightedObject.bagPositionIndices);
        }
        else if (this.highlightedObject != potentialHighlightedObject)
        {
            this.highlightedObject.UnHighlightObject();
            this.highlightedObject = potentialHighlightedObject;
            this.highlightedObject.HighlightObject();
            this.AdjustCursorToHighlightedObject(this.highlightedObject.bagPositionIndices);
        }
    }

    private void MoveOffset(Vector2Int offset)
    {
        this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x + offset.x, this.currentPositionInBag.y - offset.y);
        this.MoveCursor();
    }

    private void RotateObject(bool clockwise)
    {
        if (this.grabbedObject == null)
        {
            return;
        }

        Vector2Int clampOffset = this.grabbedObject.Rotate(clockwise);
        this.MoveOffset(clampOffset);
        this.grabbedObject.RefreshHighlightTiles();
    }

    private void PickUp()
    {
        InventoryObject potentialObject = this.gameBags[this.currentAssociatedBag].GetContentAtIndex(this.currentPositionInBag);

        if (potentialObject != null)
        {
            Vector3 objectMovePosition = new Vector3(this.cursorTransform.position.x + this.grabOffset,
                                                     this.cursorTransform.position.y + grabOffset, -0.15f);

            this.grabbedObject = potentialObject;
            this.grabbedObject.PickUpObject(objectMovePosition);
        }
        else
        { 
            //Play sound for invalid move
        }
    }

    private void PutDown()
    {
        if (this.grabbedObject.IsValidDropSpot() == false)
        {
            return;
        }

        this.grabbedObject.PlaceObjectInBag(this.cursorTransform.position);
        this.grabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.MoveLeft();
        }        
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.RotateObject(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.RotateObject(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.grabbedObject == null)
            {
                this.PickUp();
            }
            else
            {
                this.PutDown();
            }
        }
        
        if (this.grabbedObject != null || this.highlightedObject != null)
        {
            this.cursorSprite.enabled = false;
        }
        else
        {
            this.cursorSprite.enabled = true;
        }        
    }
}
