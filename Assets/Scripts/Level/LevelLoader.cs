using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public bool debug;

    [Header("Pre-Game Data")]
    [SerializeField] string loadObjectName;
    [SerializeField] DialoguePlayer dialoguePlayer; 
    [SerializeField] GameMaster gameMaster;

    [Header("In-Game Data, Don't edit!")]
    public Customer customerData;

    private void Start(){
        
        if(debug == false)
            customerData = GameObject.Find(loadObjectName).GetComponent<CustomerContainer>().customer;

        print($"Loading {customerData.customerName}!");

        dialoguePlayer.currentCustomer = customerData;

        gameMaster.OnLoadComplete();
    }


}
