using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSuccessCalculator : MonoBehaviour
{
    public float maxPointPerSuccess;
    public float pointForCorrectIngredientOnly;
    public float pointForCompleteFail;

    public PotionContainer container;

    private ItemContainer ExistsInPotion(Potion potion, Item target){
        
        foreach (ItemContainer item in potion.items)
        {
            if(item.item.name == target.name)
                return item;
        }

        return null;
    }

    private float IsCheck(Potion potion,PotionRequirement requirement){
        ItemContainer exists = ExistsInPotion(potion,requirement.item);
        if(exists == null){
            return pointForCompleteFail;
        }

        
        if(requirement.itemType == exists.itemType){
            return maxPointPerSuccess;
        }
        return pointForCorrectIngredientOnly;
    }

    private float IsNotCheck(Potion potion,PotionRequirement requirement){
        ItemContainer exists = ExistsInPotion(potion,requirement.item);
        if(exists == null){
            return pointForCompleteFail;
        }
    
        if(requirement.itemType != exists.itemType){
            return maxPointPerSuccess;
        }
        return pointForCorrectIngredientOnly;
    }

    private float LessThan(Potion potion,PotionRequirement requirement){
        ItemContainer exists = ExistsInPotion(potion,requirement.item);
        int amountInPotion = 0;

        if(exists == null){
            return pointForCompleteFail;
        }

        foreach (ItemContainer item in potion.items)
        {
            if(item.item.name == exists.item.name){
                amountInPotion++;
            }

        }

        if(amountInPotion <= requirement.numberDataForNumRequirements){
            return maxPointPerSuccess;
        }
        return pointForCorrectIngredientOnly; 
    }

    private float ExaclyNumber(Potion potion,PotionRequirement requirement){
        ItemContainer exists = ExistsInPotion(potion,requirement.item);
        int amountInPotion = 0;

        if(exists == null){
            return pointForCompleteFail;
        }

        foreach (ItemContainer item in potion.items)
        {
            if(item.item.name == exists.item.name)
                amountInPotion++;
        }

        if(amountInPotion == requirement.numberDataForNumRequirements){
            return maxPointPerSuccess;
        }
        
        return pointForCorrectIngredientOnly; 
    }

    private float MoreThan(Potion potion,PotionRequirement requirement){
        ItemContainer exists = ExistsInPotion(potion,requirement.item);
        int amountInPotion = 0;
        
        if(exists == null){
            return pointForCompleteFail;
        }

        foreach (ItemContainer item in potion.items)
        {
            if(item.item.name == exists.item.name)
                amountInPotion++;
        }

        if(amountInPotion >= requirement.numberDataForNumRequirements){
            return maxPointPerSuccess;
        }
        return pointForCorrectIngredientOnly;   
    }

    public float GetSuccessPercentage(Customer customer){
        Potion result = container.potion;
        List<PotionRequirement> potionRequirements = customer.requirements;
        float currentPoints = 0;
        float maxPoints = maxPointPerSuccess * potionRequirements.Count;

        foreach (PotionRequirement requirement in potionRequirements)
        {
            switch (requirement.checkType)
            {
                case PotionCorrrectnessCheckType.Is:
                    currentPoints += IsCheck(result,requirement);
                    break;
                case PotionCorrrectnessCheckType.IsNot:
                    currentPoints += IsNotCheck(result,requirement);
                    break;
                case PotionCorrrectnessCheckType.Minimum:
                    currentPoints += MoreThan(result,requirement);
                    break;
                case PotionCorrrectnessCheckType.ExactlyAt:
                    currentPoints += ExaclyNumber(result,requirement);
                    break;
                case PotionCorrrectnessCheckType.Maximum:
                    currentPoints += LessThan(result,requirement);
                    break;
                default:
                    currentPoints += 0;
                    break;
            }        
        }
        float r = currentPoints / maxPoints * 100;
        print(r + " " + maxPoints + " " + currentPoints);
        return r;
    }
}
