using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{   
    [SerializeField] GameObject prefab;
    
    [SerializeField] Mesh gasMesh;
    [SerializeField] Mesh liquidMesh;
    [SerializeField] Mesh dustMesh;

    public GameObject GiveOutput(Machine machine){

        MonoItem item = machine.outputItem;

        if(item == null)
            return null;
            
        Vector3 outputPos = machine.outputDistanceToMachine + machine.transform.position;
        GameObject newObject = Instantiate(prefab,outputPos,Quaternion.identity);

        switch (item.currentState)
        {
            case ItemType.gas:
                newObject.GetComponent<MeshFilter>().mesh = gasMesh;
                break;
            case ItemType.liquid:
                newObject.GetComponent<MeshFilter>().mesh = liquidMesh;
                break;
            case ItemType.dust:
                newObject.GetComponent<MeshFilter>().mesh = dustMesh;
                break;
            case ItemType.solid:
                newObject.GetComponent<MeshFilter>().mesh = item.item.solidMesh;
                break;
            default:
                print("No state found for object, can't build it!");
                break;
        }

        newObject.GetComponent<MeshRenderer>().material = item.item.itemMaterial;
        
        MonoItem newMono = newObject.GetComponent<MonoItem>();
        newMono.item = machine.outputItem.item;
        newMono.currentState = machine.outputItem.currentState;
        machine.outputItem = null;

        MeshCollider collider = newObject.AddComponent<MeshCollider>();
        collider.convex = true;
        return newObject;
    }
}
