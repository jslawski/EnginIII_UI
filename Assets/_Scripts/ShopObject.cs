using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopObject : MonoBehaviour
{
    public SpriteRenderer objectSprite;
    public GameObject pointObject;

    private void Awake()
    {
        this.objectSprite = GetComponent<SpriteRenderer>();
        this.pointObject = this.gameObject.transform.GetChild(0).gameObject;
    }
}
