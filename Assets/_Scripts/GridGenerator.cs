using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{   
    [SerializeField]
    private GameObject gridTilePrefab;
    [SerializeField]
    private Vector2 gridDimensions;

    private Vector2 startingPosition;

    //private float increment = 0.43f;
    

    // Start is called before the first frame update
    void Awake()
    {
        this.startingPosition = this.gameObject.transform.position;
        this.CreateGrid();
    }

    private void CreateGrid()
    {
        for (float i = 0; i < gridDimensions.y; i++)
        {
            for (float j = 0; j < gridDimensions.x; j++)
            {
                Vector3 instantiationPosition = new Vector3(this.startingPosition.x + j, 
                                                            this.startingPosition.y - i, 0.0f);
                Instantiate(this.gridTilePrefab, instantiationPosition, new Quaternion(), this.gameObject.transform);
            }
        }
    }
}
