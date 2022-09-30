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

    public void PlaceInBag(InventoryObject placedObject, Vector2Int bagIndices)
    { 
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
