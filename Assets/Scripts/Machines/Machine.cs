using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [Header("Pre-Game Stats")]
    public Vector3 outputDistanceToMachine;
    [SerializeField] List<ItemType> allowedStates;
    [SerializeField] ItemType output;
    [SerializeField] float timeToConvert;
    [SerializeField] ItemGenerator outputGenerator;
    

    [Header("In-Game Stats, Don't edit!")]
    public bool occupied;
    public MonoItem outputItem;
    [SerializeField] MonoItem activeItem;

    public GameObject GenerateOutput(){
        if(occupied)
            return null;

        return outputGenerator.GiveOutput(this);
    }

    private MonoItem CreateMono(){
        MonoItem newItem = gameObject.AddComponent<MonoItem>();
        newItem.item = activeItem.item;
        newItem.currentState = output;
        return newItem;
    }

    public IEnumerator Convert(){
        outputItem = CreateMono();
        Destroy(activeItem.gameObject);
        
        yield return new WaitForSeconds(timeToConvert);
        occupied = false;
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
