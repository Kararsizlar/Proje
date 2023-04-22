using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [Header("Pre-Game Stats")]
    [SerializeField] List<ItemType> allowedStates;
    [SerializeField] ItemType output;
    [SerializeField] float timeToConvert;

    [Header("In-Game Stats, Don't edit!")]
    [SerializeField] bool occupied;
    [SerializeField] MonoItem activeItem;
    [SerializeField] MonoItem outputItem;

    private MonoItem CreateOutput(){
        MonoItem newItem = gameObject.AddComponent<MonoItem>();
        newItem.item = activeItem.item;
        newItem.currentState = output;
        return newItem;
    }

    public IEnumerator Convert(){
        yield return new WaitForSeconds(timeToConvert);
        activeItem.currentState = output;
        occupied = false;
        outputItem = CreateOutput();
        Destroy(activeItem.gameObject);
        activeItem = null;

        print($"Converted {activeItem}! new state is {output}.");
    }

    public void OnNewItem(MonoItem newItem){
        if(occupied){
            print("Can't Convert, occupied.");
            return;
        }
    
        if(allowedStates.Contains(newItem.currentState) == false){
            print($"Can't convert this, wrong state. Current state is: {newItem.currentState}");
            return;
        }

        if(Item.CheckAllow(newItem.item,output) == false){
            print($"This object can't be converted to {output}");
            return;
        }


        activeItem = newItem;
        occupied = true;
        StartCoroutine(Convert());
    }
}
