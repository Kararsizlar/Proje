using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [Header("Pre-Game Stats")]
    public Vector3 outputDistanceToMachine;
    public GameObject outputPrefab;

    [Header("In-Game Stats, Don't edit!")]
    public GameObject outputObject;


    public virtual GameObject GenerateOutput(){
        print("Default GenerateOutput behaviour, null return.");
        return null;
    }

    public virtual void OnNewItem(MonoItem newItem){
        print("Default OnNewItem behaviour.");
    }
}
