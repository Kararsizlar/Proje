using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] CanvasGroup levelAcceptGroup;
    [SerializeField] TMPro.TextMeshProUGUI summaryText;
    [SerializeField] TMPro.TextMeshProUGUI acceptButtonName;
    [SerializeField] CustomerContainer persistentDataContainer;
    [SerializeField] string loadObjectName;

    public Customer currentCustomer;

    private void SetCanvasGroup(bool value){       
        levelAcceptGroup.interactable = value;
        levelAcceptGroup.blocksRaycasts = value;

        levelAcceptGroup.alpha = value == true ? 1 : 0;
    }

    public void UpdateCanvasNewCustomer(){
        SetCanvasGroup(true);
        acceptButtonName.text = $"{currentCustomer.customerName}'ın görevine başla.";
        summaryText.text = currentCustomer.levelSelectDialogue;
    }

    private void Start(){
        persistentDataContainer = GameObject.Find(loadObjectName).GetComponent<CustomerContainer>();
    }

    public void AcceptLevel(){
        persistentDataContainer.customer = currentCustomer;
        SceneManager.LoadScene("GameScene");
    }
}
