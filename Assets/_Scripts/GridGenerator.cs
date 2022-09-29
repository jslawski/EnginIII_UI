using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{       
    private Transform[] gridTiles;
    
    public Vector2Int gridDimensions;
    public Vector2 startingPosition;    

    // Start is called before the first frame update
    void Awake()
    {
        this.startingPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        this.gridTiles = this.GetAllChildren();
        this.startingPosition = this.gameObject.transform.position;
        this.CreateGrid();
    }

    private Transform[] GetAllChildren()
    {
        List<Transform> childList = new List<Transform>();

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            childList.Add(this.gameObject.transform.GetChild(i));
        }

        return childList.ToArray();
    }

    private void CreateGrid()
    {
        int processedTiles = 0;

        for (float i = 0; i < gridDimensions.y; i++)
        {
            for (float j = 0; j < gridDimensions.x; j++)
            {
                Vector3 transformPosition = new Vector3(this.startingPosition.x + j, 
                                                            this.startingPosition.y - i, 0.0f);        

                this.gridTiles[processedTiles].position = transformPosition;
                processedTiles++;
            }
        }
    }
}
