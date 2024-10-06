using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    public Image image;
    public Color selectedColor, notSelectedColor;

    public InventoryItem InventoryItem =>
        gameObject.transform.childCount > 0 ? gameObject.transform.GetChild(0).GetComponent<InventoryItem>() : null;

    [HideInInspector] public int index;
    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (transform.childCount == 0)
        {
            SetInventoryItem(newItem);
            return;
        }


        int newItemPreviousIndex = newItem.currentSlotPos;
        InventoryItem currentItem = this.GetComponentInChildren<InventoryItem>();

        SetInventoryItem(newItem);

        InventoryManager.Instance.InventorySlots[newItemPreviousIndex - 1].SetInventoryItem(currentItem);

    }

    public void SetInventoryItem(InventoryItem item)
    {
        //InventoryItem = item;

        item.gameObject.transform.SetParent(transform, false);
        item.parentAfterDrag = transform;
        item.currentSlotPos = this.index;
    }

}
