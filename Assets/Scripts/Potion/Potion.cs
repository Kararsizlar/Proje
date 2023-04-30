using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Potion
{
    public string potionName;
    public List<ItemEffect> items  = new List<ItemEffect>();
}
