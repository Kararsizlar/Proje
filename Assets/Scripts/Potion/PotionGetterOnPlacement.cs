using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGetterOnPlacement : MonoBehaviour
{
    public PotionContainer container;

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer == 8){//potion layer = 8
            container.potion = collision.gameObject.GetComponent<PotionContainer>().potion;
        }
    }   
}
