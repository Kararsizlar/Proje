using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoItem : MonoBehaviour
{
    [Header("Regular data")]
    public Item item;

    [Header("In-Game Data, dont edit!")]
    public ItemType currentState;

    private void Awake(){
        if(item != null) 
        currentState = item.defaultType;    
        //Nullcheck so machine outputs will not give error. Start overrides, we don't want it.
    }
}
