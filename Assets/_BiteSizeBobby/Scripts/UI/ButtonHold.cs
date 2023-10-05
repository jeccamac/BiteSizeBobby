using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonDown;
    public UnityEvent invokeMethod1; //reveal in editor
    PlayerController player;
    private void Start() 
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("button is pressed");
        isButtonDown = true;
        player._isMoving = true;
        invokeMethod1.Invoke();
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Debug.Log("no button input");
        isButtonDown = false;
        player._isMoving = false;
    }
}
