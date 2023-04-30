using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemGetter : MonoBehaviour
{
    [Header("Pre-Game Data")]
    [SerializeField] ObjectHolder holder;
    [SerializeField] Machine machine;
    [SerializeField] float minDistanceToTrigger;
    
    [Header("In-Game Data, dont edit!")]
    [SerializeField] Holdable currentItem;

    private float GetDistance(){
        Vector3 myPos = transform.position;
        Vector3 objPos = holder.currentBody.position;

        return (myPos - objPos).magnitude;
    }

    private void TryFeed(MonoItem item){
        if(item != null)
            machine.OnNewItem(item);
    }

    private void Update(){
        if(holder.currentObject != null){
            float distance = GetDistance();
            if(distance < minDistanceToTrigger)
                currentItem = holder.currentObject;
            else
                currentItem = null;
        }
        else{
            if(currentItem != null)
                TryFeed(currentItem.itemData);

            currentItem = null;
        }
    }
}
