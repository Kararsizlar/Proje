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
        
        GameObject clickedObject = results[0].collider.gameObject;

        if(clickedObject.layer == 6 || clickedObject.layer == 8)//Holdable
            return clickedObject;
        
        if(clickedObject.layer == 7)
            return clickedObject.GetComponent<Machine>().GenerateOutput();

        
        
        return null; //unreachable, no problems
    }
}
