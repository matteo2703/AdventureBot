using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public event Action<Slot> OnItemClick;
    public bool empty;
    public KeyValuePair<InventoryItem, int> item;
    public int quantity;

    [Header("Slot Parts")]
    [SerializeField] private Image selectionImage;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI quantityText;
    public void OnPointerClick(PointerEventData pointer)
    {
        if(pointer.button == PointerEventData.InputButton.Left)
        {
            OnItemClick?.Invoke(this);
            Select(true);
        }
    }
    public void Select(bool select)
    {
        selectionImage.gameObject.SetActive(select);
    }
    public void SetData(KeyValuePair<InventoryItem, int> item)
    {
        this.item = item;

        quantity = item.Value;
        Select(false);
        itemIcon.sprite = item.Key.item.itemIcon;
        nameText.text = item.Key.item.itemName;
        quantityText.text = item.Value.ToString();
        empty = false;
    }
    public void ResetData()
    {
        Select(false);
        itemIcon.gameObject.SetActive(false);
        nameText.text = "";
        quantityText.text = "";
        empty = true;
    }
    public void UpdateQuantity()
    {
        item = new KeyValuePair<InventoryItem, int>(item.Key, item.Value - 1);
        quantity = item.Value;
        quantityText.text = quantity.ToString();
    }
}
