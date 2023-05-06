using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSuccessCalculator : MonoBehaviour
{
    public Potion testTarget;
    public Potion testInput;

    [Header("Toplamın 100 olduğundan emin ol!")]
    public SuccessMultipliers multipliers;
    
    [Header("Efekt güç kontrolü fonksiyonunun değerleri")]
    public StrengthVariables strengthValues;

    private int GetEffectAmount(Potion p){
        return p.items.Count;
    }

    private float CalculateEffectAmountSuccess(Potion target,Potion input){
        int targetEffectAmount = GetEffectAmount(target);
        int inputEffectAmount = GetEffectAmount(input);
        int diff = System.Math.Abs(targetEffectAmount - inputEffectAmount);
        float pointPerEffect = 100 / (float)targetEffectAmount;

        float punishment = diff * pointPerEffect;
        float returnValue = 100 - punishment;
        print("amount = " + returnValue);
        return 100 - punishment;
    }

    private float CalculateEffectStrengthSuccess(Potion target,Potion input){

        float GetBonusPoint(){
            float containBonusPoint = 0;
            foreach (ItemEffect effect in input.items)
            {
                bool hit = false;
                foreach (ItemEffect targetEffect in target.items)
                {
                    if(effect.effect == targetEffect.effect){
                        containBonusPoint += strengthValues.cointainsBonusPoint;
                        hit = true;
                    }
                }
                if(hit == false)
                    containBonusPoint -= strengthValues.cointainsBonusPoint;
            }

            containBonusPoint = Mathf.Clamp(containBonusPoint,0,containBonusPoint);
            float returnValue = containBonusPoint / (target.items.Count * strengthValues.cointainsBonusPoint) * strengthValues.cointainsBonusPoint;
            print("bonus = " + returnValue);
            return returnValue;
        }

        float calculateEffectStrength(ItemEffect effect){

            foreach (ItemEffect targetEffect in target.items)
            {
                if(effect.effect == targetEffect.effect){
                    int targetStr = targetEffect.strength;
                    int diff = System.Math.Abs(targetStr - effect.strength);
                    float pointPerEffect = 100 / (float)targetStr;
                    float returnValue = Mathf.Clamp(100 - (diff * pointPerEffect),0,100);
                    print(effect + " = " + returnValue);
                    return returnValue;
                }
            }
            print(effect + " = 0");
            return 0;
        }

        float existInPotionBonus = GetBonusPoint();
        float strengthPoints = 0;

        foreach (ItemEffect effect in input.items)
        {
            strengthPoints += calculateEffectStrength(effect);
        }

        strengthPoints = strengthPoints / 100 / target.items.Count * (100 - strengthValues.cointainsBonusPoint);
        float returnValue = strengthPoints + existInPotionBonus;
        print("str = " + returnValue);
        return returnValue;
    }

    public float CalculateSuccess(Potion target,Potion input){
        float effectAmountPoint = CalculateEffectAmountSuccess(target,input) * multipliers.effectAmountMultiplier / 100;
        float strengthPoint = CalculateEffectStrengthSuccess(target,input) * multipliers.effectStrengthRateMultiplier / 100;
        float total = strengthPoint + effectAmountPoint;

        print(target.potionName + " fits " + input.potionName + " by: %" + total);
        return total;
    }

    private void Start(){
        CalculateSuccess(testTarget,testInput);
    }
}

[System.Serializable]
public class SuccessMultipliers{
    public float effectAmountMultiplier;
    public float effectStrengthRateMultiplier;
}

[System.Serializable]
public class StrengthVariables{
    public float cointainsBonusPoint;
}
