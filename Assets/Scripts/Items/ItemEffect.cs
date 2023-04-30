using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public int strength;
    public Effect effect;
}

public enum Effect{
    no_effect = 0,
    effect_1 = 1,
    effect_2 = 2,
    effect_3 = 3,
    effect_4 = 4,
}


