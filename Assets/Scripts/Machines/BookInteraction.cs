using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BookInteraction : MonoBehaviour , IPointerDownHandler
{
    public GameMaster gameMaster;
    public CanvasGroup bookPanel;
    public RectTransform elementParent;
    public GameObject prefab;

    private bool initialized = false;

    public void OnBookClick(){
        bookPanel.alpha = 1;
        bookPanel.blocksRaycasts = true;
        bookPanel.interactable = true;
    }

    public void OnReturnButton(){
        bookPanel.alpha = 0;
        bookPanel.blocksRaycasts = false;
        bookPanel.interactable = false;       
    }

    public void OnPointerDown(PointerEventData eventData){
        OnBookClick();

        if(initialized == false)
            Initialize();
    }

    private void Initialize(){
        Customer c = gameMaster.customer;
        initialized = true;
        
        foreach (string directive in c.requirementBook.sentences)
        {
            GameObject newGO = Instantiate(prefab);
            newGO.transform.SetParent(elementParent,false);
            newGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = directive;
        }
    }
}
