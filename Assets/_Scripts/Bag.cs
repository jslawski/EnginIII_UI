using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private bool initialized = false;

    private InventoryObject[,] bagContentFlags;
    
    public GridGenerator bindedGrid;

    private void Awake()
    {
        if (initialized == true)
        {
            return;
        }

        this.bindedGrid = GetComponentInChildren<GridGenerator>();

        this.bagContentFlags = new InventoryObject[(int)bindedGrid.gridDimensions.x, (int)bindedGrid.gridDimensions.y];

        this.initialized = true;
    }

    public void PlaceInBag(InventoryObject placedObject)
    {
        int yEndIndex = placedObject.bagPositionIndices.y + placedObject.objectDimensions.y;
        int xEndIndex = placedObject.bagPositionIndices.x + placedObject.objectDimensions.x;

        for (int i = placedObject.bagPositionIndices.y, object_i = 0; i < yEndIndex; i++, object_i++)
        {
            for (int j = placedObject.bagPositionIndices.x, object_j = 0; j < xEndIndex; j++, object_j++)
            {
                if (placedObject.objectMatrix[object_j, object_i] == true)
                {
                    this.bagContentFlags[j, i] = placedObject;
                }
            }
        }
    }

    public void RemoveFromBag(InventoryObject removedObject)
    {
        int yEndIndex = removedObject.bagPositionIndices.y + removedObject.objectDimensions.y;
        int xEndIndex = removedObject.bagPositionIndices.x + removedObject.objectDimensions.x;

        for (int i = removedObject.bagPositionIndices.y, object_i = 0; i < yEndIndex; i++, object_i++)
        {
            for (int j = removedObject.bagPositionIndices.x, object_j = 0; j < xEndIndex; j++, object_j++)
            {
                if (removedObject.objectMatrix[object_j, object_i] == true)
                {
                    this.bagContentFlags[j, i] = null;
                }
            }
        }
    }

    public InventoryObject GetContentAtIndex(Vector2Int indices)
    {
        return (this.bagContentFlags[indices.x, indices.y]);
    }

    private void PrintBagContents()
    {
        string debugString = this.gameObject.name + " Contents:\n";

        for (int i = 0; i < this.bindedGrid.gridDimensions.y; i++)
        {
            debugString += "[";
            for (int j = 0; j < this.bindedGrid.gridDimensions.x; j++)
            {                
                debugString += (this.bagContentFlags[j, i] == null) ? " 0 " : " 1 ";
            }
            debugString += "]\n";
        }

        Debug.LogError(debugString);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            this.PrintBagContents();
        }
    }
}
