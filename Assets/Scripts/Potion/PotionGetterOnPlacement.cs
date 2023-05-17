using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGetterOnPlacement : MonoBehaviour
{
    public PotionContainer container;

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer == 6 && collision.gameObject.tag == "Potion"){
            Potion p = collision.gameObject.GetComponent<PotionContainer>().potion;
            container.potion = new Potion();
            container.potion.potionName = p.potionName;
            foreach (ItemContainer item in p.items)
            {
                container.potion.items.Add(item);
            }
        }
    }   
}
