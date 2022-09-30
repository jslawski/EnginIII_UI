using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSprite : MonoBehaviour
{
    Sprite objectSprite;

    // Start is called before the first frame update
    void Awake()
    {
        this.objectSprite = GetComponent<SpriteRenderer>().sprite;
        this.Snap(Orientation.Deg0);
    }

    public void Snap(Orientation newOrientation)
    {
        if (newOrientation == Orientation.Deg0 || newOrientation == Orientation.Deg180)
        {
            this.gameObject.transform.localPosition = new Vector3(this.objectSprite.bounds.extents.x, -this.objectSprite.bounds.extents.y, 0.0f);
        }
        else
        {
            this.gameObject.transform.localPosition = new Vector3(this.objectSprite.bounds.extents.y, -this.objectSprite.bounds.extents.x, 0.0f);
        }
    }
}
