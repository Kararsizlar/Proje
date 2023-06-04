using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleConverter : Machine
{
    public bool occupied;

    [Header("Machine Data")]
    [SerializeField] ObjectHolder holder;
    [SerializeField] ItemType state1,state2;
    [SerializeField] ItemType outState1,outState2;
    [SerializeField] float timeToConvert;

    [SerializeField] ItemContainer container;
    [SerializeField] string outputName;
    [SerializeField] Mesh outputMesh1,outputMesh2;

    public ItemContainer currentHoldable;

    private ItemContainer CreateContainer(ItemContainer holdable){
        holder.holding = false;
        container.item = holdable.item;

        if(holdable.itemType == state1)
            container.itemType = outState1;
        if(holdable.itemType == state2)
            container.itemType = outState2;

        Destroy(holder.selectedObject);
        holder.LetGoOfObject(false);
        return container;
    }

    public void Wait(){
        occupied = false;
    }

    public override GameObject GenerateOutput()
    {
        if(occupied || output == null)
            return null;
            
        GameObject outputObject = Instantiate(outputPrefab,outputPos,Quaternion.identity);
        outputObject.GetComponent<MeshRenderer>().material = output.item.itemMaterial;
        GenerateCollider();
        ItemContainer h = outputObject.GetComponent<ItemContainer>();        
        h.original = false;

        h.item = output.item;
        h.itemType = output.itemType;
        output = null;
        return outputObject;

        void GenerateCollider(){
            BoxCollider collider = outputObject.AddComponent<BoxCollider>();
            MeshFilter meshFilter = outputObject.GetComponent<MeshFilter>();
            Mesh m;

            if(output.itemType == ItemType.solid)
                m = currentHoldable.item.solidMesh;
            else
                m = output.itemType == state1 ? outputMesh1 : outputMesh2;          
            
            meshFilter.mesh = m;
        }
    }

    public override void OnNewItem()
    {
        if(occupied){
            print("Can't Convert, occupied.");
            return;
        }

        bool canConvert = state1 == holder.currentHoldable.itemType || state2 == holder.currentHoldable.itemType;
        bool canConvertTo = Item.CheckAllow(holder.currentHoldable.item,outState1) || Item.CheckAllow(holder.currentHoldable.item,outState2);
        
        if(canConvert == false)
            return;

        if(canConvertTo == false)
            return;

        output = CreateContainer(holder.currentHoldable);
        occupied = true;

        Invoke("Wait",timeToConvert);
    }
}
