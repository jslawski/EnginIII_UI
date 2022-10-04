using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryObjectDetailsDict
{
    public static Dictionary<string, InventoryObjectDetails> dict;

    public static void SetupDictionary()
    {
        dict = new Dictionary<string, InventoryObjectDetails>();

        Axe();
        Bass();
        Boomerang();
        Herb();
        Landmine();
        Pickaxe();
        Soda();
        Spear();
        Sword();
    }

    private static void Axe()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/axe");
        inventoryObject.objectDimensions = new Vector2Int(2, 4);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = false;
        objectMatrix[0, 2] = false;
        objectMatrix[0, 3] = false;

        objectMatrix[1, 0] = true;
        objectMatrix[1, 1] = true;
        objectMatrix[1, 2] = true;
        objectMatrix[1, 3] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("axe", inventoryObject);
    }

    private static void Bass()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/bass");
        inventoryObject.objectDimensions = new Vector2Int(1, 3);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;
        objectMatrix[0, 2] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("bass", inventoryObject);
    }

    private static void Boomerang()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/boomerang");
        inventoryObject.objectDimensions = new Vector2Int(2, 2);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = false;

        objectMatrix[1, 0] = true;
        objectMatrix[1, 1] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("boomerang", inventoryObject);
    }

    private static void Herb()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/herb");
        inventoryObject.objectDimensions = new Vector2Int(1, 1);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("herb", inventoryObject);
    }

    private static void Landmine()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/landmine");
        inventoryObject.objectDimensions = new Vector2Int(3, 2);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;

        objectMatrix[1, 0] = true;
        objectMatrix[1, 1] = true;

        objectMatrix[2, 0] = true;
        objectMatrix[2, 1] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("landmine", inventoryObject);
    }

    private static void Pickaxe()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/pickaxe");
        inventoryObject.objectDimensions = new Vector2Int(3, 3);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = false;
        objectMatrix[0, 2] = false;

        objectMatrix[1, 0] = true;
        objectMatrix[1, 1] = true;
        objectMatrix[1, 2] = true;

        objectMatrix[2, 0] = true;
        objectMatrix[2, 1] = false;
        objectMatrix[2, 2] = false;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("pickaxe", inventoryObject);
    }

    private static void Soda()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/soda");
        inventoryObject.objectDimensions = new Vector2Int(1, 2);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("soda", inventoryObject);
    }

    private static void Spear()
    {
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/spear");
        inventoryObject.objectDimensions = new Vector2Int(1, 6);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;
        objectMatrix[0, 2] = true;
        objectMatrix[0, 3] = true;
        objectMatrix[0, 4] = true;
        objectMatrix[0, 5] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("spear", inventoryObject);
    }

    private static void Sword()
    {        
        InventoryObjectDetails inventoryObject = new InventoryObjectDetails();
        inventoryObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/sword");
        inventoryObject.objectDimensions = new Vector2Int(1, 4);

        bool[,] objectMatrix = new bool[inventoryObject.objectDimensions.x, inventoryObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;
        objectMatrix[0, 2] = true;
        objectMatrix[0, 3] = true;

        inventoryObject.originalObjectMatrix = objectMatrix;

        dict.Add("sword", inventoryObject);
    }
}
