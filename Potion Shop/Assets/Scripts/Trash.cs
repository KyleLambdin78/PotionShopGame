﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Trash : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<PotionDisplay>() != null)
        {
            Destroy(eventData.pointerDrag);
        }
    }
}
