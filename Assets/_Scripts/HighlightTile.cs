using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTile : MonoBehaviour
{
    public bool isEmpty = false;
    
    private SpriteRenderer tileSprite;

    [SerializeField]
    private Sprite emptySprite;
    [SerializeField]
    private Sprite placedSprite;
    [SerializeField]
    private Sprite highlightedSprite;
    [SerializeField]
    private Sprite validSprite;
    [SerializeField]
    private Sprite invalidSprite;

    private void Awake()
    {
        this.tileSprite = GetComponent<SpriteRenderer>();        
    }

    public void SetEmpty()
    {
        this.tileSprite.sprite = emptySprite;
        this.isEmpty = true;
    }

    public void SetPlaced()
    {
        this.tileSprite.sprite = placedSprite;
    }

    public void SetHighlighted()
    {
        this.tileSprite.sprite = highlightedSprite;
    }

    public void SetValid()
    {
        this.tileSprite.sprite = validSprite;
    }

    public void SetInvalid()
    {
        this.tileSprite.sprite = invalidSprite;
    }
}
