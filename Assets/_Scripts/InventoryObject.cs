using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    private SpriteRenderer objectSprite;
    public Vector2Int objectDimensions;
    public bool[] objectMatrix;    

    private bool placed = false;
    private Bag associatedBag = null;

    

    private void Awake()
    {
        this.objectSprite = GetComponent<SpriteRenderer>();
    }

    public void PickUp()
    {
        this.placed = false;
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
