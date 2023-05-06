using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorButtton : MonoBehaviour
{
    [SerializeField] LevelSelector levelSelector;

    public void PickLevel(Customer customer){
        levelSelector.currentCustomer = customer;
        levelSelector.UpdateCanvasNewCustomer();
    }
}
