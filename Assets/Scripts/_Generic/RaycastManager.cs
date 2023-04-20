using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] Camera currentCamera;

    public GameObject GetObjectFromCamera(Vector3 endPos,out RaycastHit[] results,float distance,LayerMask mask){       
        Vector3 startPoint = currentCamera.transform.position;
        Vector3 direction = (endPos - startPoint).normalized;

        results = Physics.RaycastAll(startPoint,direction,distance,mask);

        if(results.Length == 0)
            return null;
        
        return results[0].collider.gameObject;
    }
}
