using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public ObjectHolder h;

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.layer == 6){
            h.LetGoOfObject(false);
            Destroy(collision.gameObject);
        }
    }
}
