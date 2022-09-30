using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState { Placed, Highlighted, Valid, Invalid }

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

        for (int i = 0; i < objectMatrix.GetLength(1); i++)
        {
            for (int j = 0; j < objectMatrix.GetLength(0); j++)
            {
                GameObject tileInstance = 
                    Instantiate(this.highlightTilePrefab, Vector3.zero, new Quaternion(), this.gameObject.transform);
                tileInstance.transform.localPosition = new Vector3(tileInstance.transform.localPosition.x + j,
                                                                   tileInstance.transform.localPosition.y - i,
                                                                   0.0f);
                
                HighlightTile highlightTileComponent = tileInstance.GetComponent<HighlightTile>();

                if (objectMatrix[j, i] == true)
                {
                    highlightTileComponent.SetPlaced();
                }
                else
                {
                    highlightTileComponent.SetEmpty();
                }

                this.tiles[j, i] = highlightTileComponent;
            }
        }
    }
    public void NotifyRotation(bool[,] objectMatrix)
    {
        for (int i = 0; i < this.tiles.GetLength(1); i++)
        {
            for (int j = 0; j < this.tiles.GetLength(0); j++)
            {
                Destroy(this.tiles[j, i].gameObject);                
            }
        }
        
        this.tiles = new HighlightTile[objectMatrix.GetLength(0), objectMatrix.GetLength(1)];

        for (int i = 0; i < objectMatrix.GetLength(1); i++)
        {
            for (int j = 0; j < objectMatrix.GetLength(0); j++)
            {
                GameObject tileInstance =
                    Instantiate(this.highlightTilePrefab, Vector3.zero, new Quaternion(), this.gameObject.transform);
                tileInstance.transform.localPosition = new Vector3(tileInstance.transform.localPosition.x + j,
                                                                   tileInstance.transform.localPosition.y - i,
                                                                   0.0f);

                HighlightTile highlightTileComponent = tileInstance.GetComponent<HighlightTile>();

                if (objectMatrix[j, i] == true)
                {
                    highlightTileComponent.SetHighlighted();
                }
                else
                {
                    highlightTileComponent.SetEmpty();
                }

                this.tiles[j, i] = highlightTileComponent;
            }
        }
    }
    

    public void NotifyStateChange(Vector2Int tileIndex, ObjectState newState)
    {
        if (this.tiles[tileIndex.x, tileIndex.y].isEmpty == true)
        {
            return;
        }
        
        switch (newState)
        {
            case ObjectState.Placed:
                this.tiles[tileIndex.x, tileIndex.y].SetPlaced();
                break;
            case ObjectState.Highlighted:
                this.tiles[tileIndex.x, tileIndex.y].SetHighlighted();
                break;
            case ObjectState.Valid:
                this.tiles[tileIndex.x, tileIndex.y].SetValid();
                break;
            case ObjectState.Invalid:
                this.tiles[tileIndex.x, tileIndex.y].SetInvalid();
                break;
        }
    }
}
