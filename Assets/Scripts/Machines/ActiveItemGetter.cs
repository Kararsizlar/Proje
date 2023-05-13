using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveItemGetter : MonoBehaviour , IPointerDownHandler
{
    [Header("Pre-Game Data")]
    [SerializeField] ObjectHolder holder;
    [SerializeField] Machine machine;

    private float GetDistance(){
        Vector3 myPos = transform.position;
        Vector3 objPos = holder.currentBody.position;

        return (myPos - objPos).magnitude;
    }

    private void TryFeed(MonoItem item){
        if(item != null)
            machine.OnNewItem(item);
    }

    public void OnPointerDown(PointerEventData eventData){
        Holdable currentItem = holder.currentObject;
        if(currentItem == null)
            return;
        else
            TryFeed(holder.currentObject.itemData);
    }
    
}
