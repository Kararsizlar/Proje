using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] RectTransform button;
    [SerializeField] Vector3 buttonStart,buttonTarget;
    public float buttonTime,buttonHideTime;

    [SerializeField] UIItemMover itemMover;

    public void MoveToScreen(){
        StartCoroutine(itemMover.IMoveUI(button,buttonTarget,buttonTime));
    }

    public void HideFromScreen(){
        StartCoroutine(itemMover.IMoveUI(button,buttonStart,buttonHideTime));
    }

    public void Quit(){
        Application.Quit();
    }
}
