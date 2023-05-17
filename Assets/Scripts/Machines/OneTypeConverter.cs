using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTypeConverter : Machine
{
    public bool occupied;


    [Header("Machine Data")]
    [SerializeField] ObjectHolder holder;
    [SerializeField] ItemType allowedState;
    [SerializeField] ItemType targetState;
    [SerializeField] float timeToConvert;

    [SerializeField] ItemContainer container;
    [SerializeField] string outputName;
    [SerializeField] Mesh outputMesh;

    private ItemContainer CreateContainer(ItemContainer holdable){
        holder.holding = false;
        container.item = holdable.item;
        container.itemType = targetState;

        Destroy(holder.selectedObject);
        holder.LetGoOfObject(false);
        return container;
    }

    public void Wait(){
        print($"Converted! new state is {output.itemType}.");
        occupied = false;
    }

    public override GameObject GenerateOutput()
    {
        if(occupied || output == null)
            return null;
        
        Vector3 outputPos = outputDistanceToMachine + transform.position;
        GameObject outputObject = Instantiate(outputPrefab,outputPos,Quaternion.identity);
        GenerateCollider();
        outputObject.GetComponent<MeshRenderer>().material = output.item.itemMaterial;
        ItemContainer holdable = outputObject.GetComponent<ItemContainer>();
        holdable.item = output.item;
        holdable.itemType = targetState;

        output = null;
        return outputObject;

        void GenerateCollider(){
            MeshCollider collider = outputObject.AddComponent<MeshCollider>();
            MeshFilter meshFilter = outputObject.GetComponent<MeshFilter>();

            Mesh m = output.itemType == ItemType.solid ? output.item.solidMesh : outputMesh;
            collider.sharedMesh = m;
            meshFilter.mesh = m;

            collider.convex = true;
        }
    }

    public override void OnNewItem()
    {
        if(occupied){
            print("Can't Convert, occupied.");
            return;
        }

        if(allowedState != holder.currentHoldable.itemType){
            print($"Can't convert this, wrong state. Current state is: {holder.currentHoldable.itemType}");
            return;
        }

        if(Item.CheckAllow(holder.currentHoldable.item,targetState) == false){
            print($"This object can't be converted to {targetState}");
            return;
        }

        output = CreateContainer(holder.currentHoldable);
        occupied = true;

        Invoke("Wait",timeToConvert);
    }
}
