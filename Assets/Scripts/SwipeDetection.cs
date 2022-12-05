using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetection : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public static Action OnSwipeUp;
    public static Action OnSwipeDown;
    public static Action OnSwipeRight;
    public static Action OnSwipeLeft;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
        {
            if(eventData.delta.y < 0)
            {
                OnSwipeDown?.Invoke();
            }
            else
            {
                OnSwipeUp?.Invoke();
            }
        }
        else
        {
            if (eventData.delta.x < 0)
            {
                OnSwipeLeft?.Invoke();
            }
            else
            {
                OnSwipeRight?.Invoke();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
