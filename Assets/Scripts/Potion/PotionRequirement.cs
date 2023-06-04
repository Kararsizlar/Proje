using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionRequirement
{
    public Item item;
    public PotionCorrrectnessCheckType checkType;
    public ItemType itemType;
    public int numberDataForNumRequirements;
}

public enum PotionCorrrectnessCheckType{
    Is = 0,
    IsNot = 1,
    Maximum = 2,
    ExactlyAt = 3,
    Minimum = 4,
}
