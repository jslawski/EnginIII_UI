using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryObjectDetailsDict
{
    public static Dictionary<string, InventoryObjectDetails> dict;

    public static void SetupDictionary()
    {
        dict = new Dictionary<string, InventoryObjectDetails>();
        Sword();
    }

    private static void Sword()
    {
        InventoryObjectDetails swordObject = new InventoryObjectDetails();
        swordObject.objectSprite = Resources.Load<Sprite>("InventoryObjects/sword");
        swordObject.objectDimensions = new Vector2Int(1, 4);

        bool[,] objectMatrix = new bool[swordObject.objectDimensions.x, swordObject.objectDimensions.y];
        objectMatrix[0, 0] = true;
        objectMatrix[0, 1] = true;
        objectMatrix[0, 2] = true;
        objectMatrix[0, 3] = true;

        swordObject.originalObjectMatrix = objectMatrix;

        dict.Add("sword", swordObject);
    }
}
