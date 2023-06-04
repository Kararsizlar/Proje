using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bell : MonoBehaviour , IPointerDownHandler
{
    public GameMaster gameMaster;
    public PotionGetterOnPlacement potionGetter;

    public void OnPointerDown(PointerEventData eventData){

        if(potionGetter.eligible)
            gameMaster.StartCoroutine(gameMaster.EndGame());
    }
}
