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
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject itemText;

    private void Awake()
    {
        
    }
    public void Start()
    {
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
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        onSlot = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        if (onSlot == false)
        {
            rectTransform.anchoredPosition = defaultPos;
        }
        else
        {
            defaultPos = rectTransform.anchoredPosition;
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

