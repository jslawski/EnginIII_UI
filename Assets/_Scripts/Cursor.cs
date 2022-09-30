using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Transform cursorTransform;

    private Bag[] gameBags;

    private Vector2Int currentPositionInBag;

    private int currentAssociatedBag = 0;

    [SerializeField]
    private InventoryObject grabbedObject;

    private void Start()
    {
        this.cursorTransform = GetComponent<Transform>();
        this.gameBags = this.gameObject.transform.parent.GetComponentsInChildren<Bag>();
        this.cursorTransform.position = new Vector3(this.gameBags[0].bindedGrid.startingPosition.x, 
                                                    this.gameBags[0].bindedGrid.startingPosition.y, -0.2f);
        this.grabbedObject.gameObject.transform.position = this.cursorTransform.position;
    }

    private void MoveRight()
    {
        if (this.currentPositionInBag.x < (this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.x - 1))
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x + 1, this.currentPositionInBag.y);
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
        if (this.currentPositionInBag.x > 0)
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x - 1, this.currentPositionInBag.y);
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
        if (this.currentPositionInBag.y > 0)
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x, this.currentPositionInBag.y - 1);
        }
        else
        { 
            //Sound for invalid move
        }

        this.MoveCursor();
    }

    private void MoveDown()
    {
        if (this.currentPositionInBag.y < (this.gameBags[this.currentAssociatedBag].bindedGrid.gridDimensions.y - 1))
        {
            this.currentPositionInBag = new Vector2Int(this.currentPositionInBag.x, this.currentPositionInBag.y + 1);
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
            this.grabbedObject.gameObject.transform.position = newPosition;
        }
    }

    private void RotateObject(bool clockwise)
    {
        if (this.grabbedObject == null)
        {
            return;
        }

        this.grabbedObject.Rotate(clockwise);
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
    }
}
