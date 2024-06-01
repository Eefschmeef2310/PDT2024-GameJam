using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 initialPosition;
    Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData) {
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        initialPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        this.transform.SetParent(parentToReturnTo);
        transform.position = initialPosition;

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter != parentToReturnTo.gameObject) {
            DropZone targetComponent = eventData.pointerEnter.GetComponent<DropZone>();
            if (targetComponent != null) {
                Destroy(gameObject);
            }
        }
    }

    // public void OnDrop(PointerEventData eventData) {
    //     // Assuming the component to check for is of type "TargetComponent"
    //     Debug.Log(eventData.pointerEnter);
    //     if (eventData.pointerEnter != null && eventData.pointerEnter != parentToReturnTo.gameObject) {
    //         DropZone targetComponent = eventData.pointerEnter.GetComponent<DropZone>();
    //         if (targetComponent != null) {
    //             Destroy(gameObject);
    //         }
    //     }
    // }
}
