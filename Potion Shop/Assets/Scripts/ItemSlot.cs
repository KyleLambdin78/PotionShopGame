using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour, IDropHandler
{
    [HideInInspector]public bool hasItem;
    [HideInInspector]public CombineItems CombineItems;
    [SerializeField] private bool isResult = false;
    [HideInInspector]public GameObject currentItem;
    void Start()
    {
        hasItem = false;
        if (transform.parent != null && isResult == false)
        {
            CombineItems = this.GetComponentInParent<CombineItems>();
        }
        
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && hasItem == false)
        {
            currentItem = eventData.pointerDrag;
            DragDrop dragDrop = currentItem.GetComponent<DragDrop>();
            dragDrop.onSlot = true;
            currentItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            hasItem = true;
            dragDrop.AssignItemSlot(this.gameObject);
            if (CombineItems != null && currentItem.GetComponent<IngredientDisplay>() != null)    
            {
                CombineItems.CreateItem(currentItem.GetComponent<IngredientDisplay>().ingredient);
            }
            
        }

  
    }
    public void ClearItems()
    {
        currentItem.GetComponent<RectTransform>().anchoredPosition = currentItem.GetComponent<DragDrop>().defaultPos;
        currentItem.GetComponent<DragDrop>().itemSlot = null;
        currentItem = null;
        hasItem = false;
        
    }
    public void RemoveItem()
    {
        if(CombineItems != null)
        {
            if(currentItem.GetComponent<IngredientDisplay>() != null)
            {
                CombineItems.currentIngredients.Remove(currentItem.GetComponent<IngredientDisplay>().ingredient);
            }
            
        }
        currentItem = null;
        hasItem = false;

    }
}
