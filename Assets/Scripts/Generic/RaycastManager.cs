using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] Camera currentCamera;

    private Camera GetCamera(){
        return Camera.current;
    }

    public GameObject GetObjectFromCamera(Vector3 endPos,out RaycastHit[] results,float distance,LayerMask mask){       
        Vector3 startPoint = currentCamera.transform.position;
        Vector3 direction = (endPos - startPoint).normalized;

        results = Physics.RaycastAll(startPoint,direction,distance,mask);

        if(results.Length == 0)
            return null;
        
        return results[0].collider.gameObject;
    }

    

    public float GetFirstCollisionDistanceFromCamera(Vector3 endPos,float maxDistance,out bool isSafe,MeshCollider collider){    
        if(currentCamera == null)
            currentCamera = GetCamera();
        

        bool IsSafe(){
            Bounds bounds = collider.bounds;
            float xSize = bounds.size.x;
            float ySize = bounds.size.y;
            float zSize = bounds.size.z;
            bool hit = Physics.BoxCast(endPos,new Vector3(xSize,ySize,zSize),Vector3.zero,Quaternion.identity);

            return !hit;
        }

        Vector3 startPoint = currentCamera.transform.position;
        Vector3 direction = (endPos - startPoint).normalized;
        RaycastHit[] results = Physics.RaycastAll(startPoint,direction,maxDistance);
        isSafe = IsSafe();

        if(results.Length < 2)
            return maxDistance;
        
        float distance = (results[1].point - currentCamera.transform.position).magnitude;

        if(distance > maxDistance)
            return maxDistance;
        
        return distance;
    }
}
