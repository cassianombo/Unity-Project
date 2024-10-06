using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventorySlot[] InventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject worldItemPrefab;
    public Item itemToSpawn;

    int selectedSlot = 0;


    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        for (int i = 0; i < InventorySlots.Length; i++)
            InventorySlots[i].index = i + 1;
    }

    private void Update()
    {
        if(Input.inputString != null)
        {
            UpdateInventory();
        }

        void UpdateInventory()
        {
            bool isNumber = int.TryParse(Input.inputString, out int selectedSlot);
            if (isNumber && selectedSlot > 0 && selectedSlot <= InventorySlots.Length)
            {
                ChangeSelectedSlot(selectedSlot - 1);
            }
        }
        
    }

    public bool AddItem(Item item)
    {
        foreach(InventorySlot slot in InventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            //Check if it is the same type
            if(itemInSlot != null 
                && itemInSlot.item == item 
                && itemInSlot.item.maxStack > itemInSlot.count)
            {
                itemInSlot.count++;
                return true;
            }

            //Found empty slot
            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public Item GetSelectedItem(bool use = false)
    {
        InventoryItem inventoryItem = InventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>();

        if (inventoryItem == null)
            return null;

        if (use)
        {
            inventoryItem.count--;
            if (inventoryItem.count <= 0)
                Destroy(inventoryItem.gameObject);
        }

        return inventoryItem.item;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        //slot.InventoryItem = inventoryItem;
        inventoryItem.InitialiseItem(item);

    }

    private void ChangeSelectedSlot(int newValue)
    {

        InventorySlots[selectedSlot].Deselect();
        InventorySlots[newValue].Select();
        selectedSlot = newValue;

    }

    public InventoryItem GetCurrentInventoryItem()
    {
        return InventorySlots[selectedSlot].InventoryItem;
    }
}