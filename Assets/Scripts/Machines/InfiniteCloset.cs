using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfiniteCloset : MonoBehaviour , IPointerDownHandler
{
    public GameMaster gameMaster;
    public GameObject itemPrefab;
    public CanvasGroup closetPanel;
    public RectTransform elementParent;

    public List<Item> items = new List<Item>();
    public Vector3 spawnPosition;
    public Transform parent;

    private bool initialized = false;

    public void SpawnNewObject(ItemContainer container){
        GameObject newObject = Instantiate(itemPrefab);
        newObject.transform.SetParent(parent);
        newObject.transform.position = spawnPosition;
        newObject.GetComponent<ItemContainer>().item = container.item;
    }

    public void OpenCloset(){
        closetPanel.alpha = 1;
        closetPanel.blocksRaycasts = true;
        closetPanel.interactable = true;
    }

    public void CloseCloset(){
        closetPanel.alpha = 0;
        closetPanel.blocksRaycasts = false;
        closetPanel.interactable = false;
    }

    public void GetObjects(){
        foreach (Item i in gameMaster.customer.closetItems)
        {
            items.Add(i);
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        OpenCloset();

        if(initialized == false)
            Initialize();
    }

    private void Initialize(){
        Customer c = gameMaster.customer;
        initialized = true;

        foreach (Item item in c.closetItems)
        {
            GameObject newGO = Instantiate(itemPrefab);
            newGO.transform.SetParent(elementParent,false);
            newGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
            ClosetItemSpawner spawner = newGO.GetComponent<ClosetItemSpawner>();
            spawner.item = item;
            spawner.closet = this;
        }
    }

    private void Start(){
        GetObjects();
    }
}
