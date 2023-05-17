using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public Item item;
    public ItemType itemType;

    private void Start(){
        if(item != null)
            itemType = item.defaultType;
    }
}
