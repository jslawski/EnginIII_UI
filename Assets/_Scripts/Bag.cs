using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private bool initialized = false;

    private bool[,] bagContentFlags;
    
    public GridGenerator bindedGrid;

    private List<InventoryObject> heldObjects;

    private void Awake()
    {
        if (initialized == true)
        {
            return;
        }

        this.bindedGrid = GetComponentInChildren<GridGenerator>();
        this.heldObjects = new List<InventoryObject>();

        this.bagContentFlags = new bool[(int)bindedGrid.gridDimensions.x, (int)bindedGrid.gridDimensions.y];

        this.initialized = true;
    }

    public void PlaceInBag(InventoryObject placedObject)
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
