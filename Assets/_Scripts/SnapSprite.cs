using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSprite : MonoBehaviour
{
    SpriteRenderer objectSprite;

    // Start is called before the first frame update
    void Awake()
    {
        this.objectSprite = GetComponent<SpriteRenderer>();
        this.Snap(Orientation.Deg0);
    }

    public void Snap(Orientation newOrientation)
    {
        if (newOrientation == Orientation.Deg0 || newOrientation == Orientation.Deg180)
        {
            this.gameObject.transform.localPosition = new Vector3(this.objectSprite.sprite.bounds.extents.x, 
                -this.objectSprite.sprite.bounds.extents.y, 0.0f);
        }
        else
        {
            this.gameObject.transform.localPosition = new Vector3(this.objectSprite.sprite.bounds.extents.y, 
                -this.objectSprite.sprite.bounds.extents.x, 0.0f);
        }
    }
}
