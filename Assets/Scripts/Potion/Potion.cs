using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Potion",fileName ="New Potion")]
public class Potion : ScriptableObject
{
    public string potionName;
    public List<ItemEffect> recipe;
}
