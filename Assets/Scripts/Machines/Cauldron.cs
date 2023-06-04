using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Machine
{
    public ObjectHolder holder;
    public GameObject currentPotion;
    private bool exported = true;

    private void AddToPotion(ItemContainer holdable){
        ItemContainer copy = currentPotion.AddComponent<ItemContainer>();

        copy.item = holdable.item;
        copy.itemType = holdable.itemType;
        currentPotion.GetComponent<PotionContainer>().potion.items.Add(copy);

        Destroy(holder.selectedObject);
        holder.LetGoOfObject(false); 
    }
    
    private ItemContainer GetPotionItem(ItemContainer item){
        Potion p = currentPotion.GetComponent<PotionContainer>().potion;
        if(p.items.Count == 0)
            return null;

        foreach (ItemContainer holdable in p.items)
        {
            if(holdable.item == item.item)
                return holdable;
        }

        return null;
    }

    public override void OnNewItem()
    {
        if(holder.currentHoldable == null)
            return;
        
        if(exported == true){
            currentPotion = Instantiate(outputPrefab);
            exported = false;
        }

        AddToPotion(holder.currentHoldable);
    }

    public override GameObject GenerateOutput()
    {
        if(output == null)
            return null;

        currentPotion.transform.position = outputPos;
        currentPotion.GetComponent<Rigidbody>().useGravity = true;
        currentPotion.GetComponent<MeshCollider>().enabled = true;
        currentPotion.GetComponent<MeshRenderer>().enabled = true;
        exported = true;
        return currentPotion;
    }
}
