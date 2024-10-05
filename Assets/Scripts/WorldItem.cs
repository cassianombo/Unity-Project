using System;
using UnityEngine;

public class WorldItem : MonoBehaviour
{

    [SerializeField]public Item Item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = Item.Image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(Item);
            Destroy(gameObject);
        }
    }

}
