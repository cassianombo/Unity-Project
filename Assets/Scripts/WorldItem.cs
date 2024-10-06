using System;
using UnityEngine;

public class WorldItem : MonoBehaviour
{

    [SerializeField]public Item Item;
    private SpriteRenderer spriteRenderer;

    public GameObject Owner;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = Item.sprite;
    }

}
