using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Machine
{
    public Potion currentPotion;

    private void AddToPotion(List<ItemEffect> effects){
        foreach (ItemEffect effect in effects)
        {
            AddToPotion(effect);    
        }
    }
    
    private void AddToPotion(ItemEffect item){
        ItemEffect itemEffect = GetEffect(item.effect);

        if(itemEffect != null)
            itemEffect.strength += item.strength;
        else{
            ItemEffect newEffect = new ItemEffect();
            newEffect.effect = item.effect;
            newEffect.strength = item.strength;
            currentPotion.items.Add(newEffect);
        }
    }
    
    private ItemEffect GetEffect(Effect effect){
        
        if(currentPotion.items.Count == 0)
            return null;

        foreach (ItemEffect i in currentPotion.items)
        {
            if(i.effect == effect)
                return i;
        }

        return null;
    }

    public override void OnNewItem(MonoItem newItem)
    {
        if(newItem.item.effects.Count == 0){
            Debug.LogWarning("This item has no effect?");
            return;
        }

        if(newItem.item.effects.Count == 1)
            AddToPotion(newItem.item.effects[0]);
        else
            AddToPotion(newItem.item.effects);
    }

    public override GameObject GenerateOutput()
    {
        Vector3 outputPos = outputDistanceToMachine + transform.position;
        GameObject outputObject = Instantiate(outputPrefab,outputPos,Quaternion.identity);
        PotionContainer potionContainer = outputObject.GetComponent<PotionContainer>();

        potionContainer.potion.items = currentPotion.items;
        potionContainer.potion.potionName = currentPotion.potionName;
        return outputObject;
    }

    private void Awake(){
        currentPotion = new Potion();
    }
}
