using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Header("UI")]
    public Image image;
    public Text countText;

    private int _count = 1;
    [HideInInspector] public int count {
        get => _count; 
        set { _count = value; RefreshCount(); } 
    }

    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;

    public int currentSlotPos= -1;

    public void RefreshCount()
    {
        countText.text = count == 1 ? "" : count.ToString();
    }

    public void InitialiseItem(Item item)
    {
        this.item = item;
        image.sprite = item.sprite;
        RefreshCount();
    }

    #region Drag Events

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentSlotPos = this.GetComponentInParent<InventorySlot>().index;
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    #endregion

}
