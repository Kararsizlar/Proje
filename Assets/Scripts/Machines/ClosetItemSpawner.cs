using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetItemSpawner : MonoBehaviour
{
    public InfiniteCloset closet;
    public GameObject prefab;
    public Item item;

    public void Spawn(){
        GameObject newGO = Instantiate(prefab);
        newGO.transform.SetParent(closet.parent);
        newGO.transform.position = closet.spawnPosition;

        //will edit.
        newGO.GetComponent<ItemContainer>().item = item;
        newGO.GetComponent<MeshFilter>().mesh = item.solidMesh;
        newGO.GetComponent<MeshRenderer>().material = item.itemMaterial;
        MeshCollider m = newGO.AddComponent<MeshCollider>();
        m.convex = true;
    }
}
