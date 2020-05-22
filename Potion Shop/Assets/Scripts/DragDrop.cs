using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    // Onslot is to make sure that the dropped object is placed on an Item Slot
    [HideInInspector] public Vector3 defaultPos;
    [HideInInspector] public bool onSlot;
    [HideInInspector] public GameObject itemSlot;
    private BoxCollider2D box;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject itemText;


    public void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        defaultPos = rectTransform.localPosition;
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        box.enabled = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        onSlot = false;
        if(itemSlot != null)
        {
            itemSlot.GetComponent<ItemSlot>().RemoveItem();
        }
        itemSlot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        box.enabled = true;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        if (onSlot == false)
        {
            rectTransform.anchoredPosition = defaultPos;
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnMouseOver()
    {
        itemText.SetActive(true);
    }
    public void OnMouseExit()
    {
        itemText.SetActive(false);
    }

}

