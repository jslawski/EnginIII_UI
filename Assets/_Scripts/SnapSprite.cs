using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSprite : MonoBehaviour
{
    Sprite objectSprite;

    // Start is called before the first frame update
    void Awake()
    {
        Sprite objectSprite = GetComponent<SpriteRenderer>().sprite;
        this.gameObject.transform.localPosition = new Vector3(objectSprite.bounds.extents.x, -objectSprite.bounds.extents.y, 0.0f);
    }
}
