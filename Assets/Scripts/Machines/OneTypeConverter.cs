using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTypeConverter : Machine
{
    public bool occupied;
    [SerializeField] Item input;
    [SerializeField] MonoItem output;

    [Header("Machine Data")]
    [SerializeField] ItemType allowedState;
    [SerializeField] ItemType targetState;
    [SerializeField] float timeToConvert;

    [SerializeField] string outputName;
    [SerializeField] Mesh outputMesh;

    private MonoItem CreateMono(){
        MonoItem newItem = gameObject.AddComponent<MonoItem>();
        newItem.item = input;
        newItem.currentState = targetState;
        return newItem;
    }

    public void Wait(){
        print($"Converted! new state is {output.currentState}.");
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
        MonoItem mono = outputObject.GetComponent<MonoItem>();
        mono.item = output.item;
        mono.currentState = output.currentState;

        input = null;
        output = null;
        return outputObject;

        void GenerateCollider(){
            MeshCollider collider = outputObject.AddComponent<MeshCollider>();
            MeshFilter meshFilter = outputObject.GetComponent<MeshFilter>();

            Mesh m = output.currentState == ItemType.solid ? output.item.solidMesh : outputMesh;
            collider.sharedMesh = m;
            meshFilter.mesh = m;

            collider.convex = true;
        }
    }

    public override void OnNewItem(MonoItem newItem)
    {
        if(input != null){
            print("Can't Convert, occupied.");
            return;
        }

        if(allowedState != newItem.currentState){
            print($"Can't convert this, wrong state. Current state is: {newItem.currentState}");
            return;
        }

        if(Item.CheckAllow(newItem.item,targetState) == false){
            print($"This object can't be converted to {targetState}");
            return;
        }

        input = newItem.item;
        output = CreateMono();
        occupied = true;
        Destroy(newItem.gameObject);
        Invoke("Wait",timeToConvert);
    }
}
