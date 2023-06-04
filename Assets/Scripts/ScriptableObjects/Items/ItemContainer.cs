using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item item;
    public ItemType itemType;

    public bool original;

    private void Start(){
        if(item != null && original)
            itemType = item.defaultType;
    }
}
