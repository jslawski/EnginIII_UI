using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlightObserver : MonoBehaviour
{
    private GameObject highlightTilePrefab;

    private HighlightTile[,] tiles;

    private void Awake()
    {
        this.highlightTilePrefab = Resources.Load<GameObject>("Prefabs/HighlightTile");        
    }

    public void SetupHighlightTiles(bool[,] objectMatrix)
    {
        this.tiles = new HighlightTile[objectMatrix.GetLength(0), objectMatrix.GetLength(1)];

        for (int i = 0; i < objectMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < objectMatrix.GetLength(1); j++)
            {
                GameObject tileInstance = 
                    Instantiate(this.highlightTilePrefab, Vector3.zero, new Quaternion(), this.gameObject.transform);
                tileInstance.transform.localPosition = new Vector3(tileInstance.transform.localPosition.x + i,
                                                                   tileInstance.transform.localPosition.y - j,
                                                                   0.0f);
                
                HighlightTile highlightTileComponent = tileInstance.GetComponent<HighlightTile>();
                highlightTileComponent.SetPlaced();
                this.tiles[i, j] = highlightTileComponent;
            }
        }
    }
}
