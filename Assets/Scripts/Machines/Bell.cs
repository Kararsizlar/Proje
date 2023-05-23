using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bell : MonoBehaviour , IPointerDownHandler
{
    public GameMaster gameMaster;

    public void OnPointerDown(PointerEventData eventData){
        gameMaster.EndGame();
    }
}
