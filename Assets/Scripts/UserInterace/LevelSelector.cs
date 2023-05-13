using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI summaryText;
    [SerializeField] Image customerImage;
    [SerializeField] CustomerContainer persistentDataContainer;
    [SerializeField] string loadObjectName;

    public Customer currentCustomer;

    public void UpdateCanvasNewCustomer(){
        summaryText.text = currentCustomer.customerName;
        customerImage.sprite = currentCustomer.levelSelectImage;
        customerImage.color = Color.white;
    }

    private void Start(){
        persistentDataContainer = GameObject.Find(loadObjectName).GetComponent<CustomerContainer>();
        if(persistentDataContainer.customer != null)
            UpdateCanvasNewCustomer();
    }

    public void AcceptLevel(){
        persistentDataContainer.customer = currentCustomer;
        SceneManager.LoadScene("GameScene");
    }
}
