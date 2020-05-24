using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Trash : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject potion = eventData.pointerDrag;
        if(potion.GetComponent<PotionDisplay>() != null)
        {
            potion.GetComponent<DragDrop>().CallItemSlot();
            Destroy(eventData.pointerDrag);
        }
    }
}
