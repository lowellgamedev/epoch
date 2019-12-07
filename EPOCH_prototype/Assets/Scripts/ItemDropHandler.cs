using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        RectTransform invUI = transform as RectTransform;

        //incomplete 
        if (!RectTransformUtility.RectangleContainsScreenPoint(invUI, Input.mousePosition)) {
            Debug.Log("Dropped");
        }
    }
}
