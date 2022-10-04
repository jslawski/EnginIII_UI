using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryParent;

    [SerializeField]
    private GameObject inventoryObjectPrefab;

    private ShopObject[] shopObjects;

    private ShopObject currentObject;

    private int currentIndex = 0;    

    // Start is called before the first frame update
    void Start()
    {
        this.shopObjects = GetComponentsInChildren<ShopObject>();
        this.currentObject = this.shopObjects[0];
        this.HighlightNewObject(0);
    }

    private void HighlightNewObject(int newIndex)
    {
        this.currentObject.pointObject.SetActive(false);

        this.currentObject = this.shopObjects[newIndex];

        this.currentObject.pointObject.SetActive(true);

        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Audio/move"), Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.inventoryParent.activeSelf == true)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (this.currentIndex > 2)
            {
                this.currentIndex -= 3;
                this.HighlightNewObject(this.currentIndex);
            }
            else
            {
                //Invalid move sound
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (this.currentIndex % 3 != 0)
            {
                this.currentIndex--;
                this.HighlightNewObject(this.currentIndex);
            }
            else
            {
                //Invalid move sound
            }            
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (this.currentIndex < 6)
            {
                this.currentIndex += 3;
                this.HighlightNewObject(this.currentIndex);
            }
            else
            {
                //Invalid move sound
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if ((this.currentIndex - 2) % 3 != 0)
            {
                this.currentIndex++;
                this.HighlightNewObject(this.currentIndex);
            }
            else
            {
                //Invalid move sound
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.OpenInventory();
        }
    }

    private void OpenInventory()
    {
        this.inventoryParent.SetActive(true);

        GameObject inventoryObjectInstance = Instantiate(this.inventoryObjectPrefab, Vector3.zero, new Quaternion());
        InventoryObject inventoryObjectComponent = inventoryObjectInstance.GetComponent<InventoryObject>();
        inventoryObjectComponent.objectName = this.shopObjects[this.currentIndex].objectSprite.sprite.name;
    }
}
