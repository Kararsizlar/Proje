using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCameraGetter : MonoBehaviour
{
    [SerializeField] LayerMask ignoreHoldable;

    public Vector3 test;

    public GameObject GetObjectFromCamera(Vector3 endPos,float distance,bool holding){       
        Collider[] results;
        test = endPos;

        if(holding == false)
            results = Physics.OverlapSphere(endPos,0.1f);
        else
            results = Physics.OverlapSphere(endPos,0.1f,ignoreHoldable);
        
        if(results.Length == 0)
            return null;

        if(results.Length == 1)
            return results[0].gameObject;
        
        else{
            foreach (Collider collider in results)
            {
                if(collider.gameObject.layer == 6)
                    return collider.gameObject;
            }

            foreach (Collider collider in results)
            {
                if(collider.gameObject.layer == 7)
                    return collider.gameObject;
            }
        }

        return null;
    }
}
