using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHolder : MonoBehaviour
{
    [Header("In-Game data")]
    [SerializeField] GameObject currentObject = null;
    [SerializeField] MeshCollider currentCollider = null;
    [SerializeField] float distanceFromCamera;
    [SerializeField] float lastSafe;

    [Header("Mouse Data")]
    [SerializeField] Vector2 mousePixels = Vector2.zero;
    [SerializeField] bool holding = false;
    [SerializeField] bool clicking = false;
    
    [Header("Camera Distance Data")]
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    [Header("Other Requirements")]
    [SerializeField] RaycastManager raycastManager;
    [SerializeField] LayerMask holdableMask;
    [SerializeField] Camera cameraObject;

    public void GetMousePos(InputAction.CallbackContext context){
        mousePixels = context.ReadValue<Vector2>();
    }

    public void GetClick(InputAction.CallbackContext context){
        clicking = context.started || context.performed;

        if(context.started)
            GetSelectedObject();
        
        if(clicking == false && holding)
            holding = false;
    }

    public void GetMiddleMouse(InputAction.CallbackContext context){
        int y = (int)context.ReadValue<Vector2>().y;
        distanceFromCamera = Mathf.Clamp(distanceFromCamera + y,minDistance,maxDistance);
    }

    private Vector3 GetMousePosWorld(Vector2 input){
        Vector3 mouseVector3 = new Vector3(input.x,input.y,cameraObject.nearClipPlane);
        return cameraObject.ScreenToWorldPoint(mouseVector3);
    }

    public void GetSelectedObject(){   
        RaycastHit[] value = null;
        currentObject = raycastManager.GetObjectFromCamera(GetMousePosWorld(mousePixels),out value,Mathf.Infinity,holdableMask);
        holding = currentObject != null; 

        if(holding)
            currentCollider = currentObject.GetComponent<MeshCollider>();
    }

    private void SetPositionOfObject(){
        Vector3 direction = (GetMousePosWorld(mousePixels) - cameraObject.transform.position).normalized;
        bool safe = false;
        float distance = raycastManager.GetFirstCollisionDistanceFromCamera(GetMousePosWorld(mousePixels),distanceFromCamera,out safe,currentCollider);
        Vector3 position;
        if(safe){
            position = direction * distance;
            lastSafe = distance;
        }
        else
            position = direction * lastSafe;

        currentObject.transform.position = cameraObject.transform.position + position;
    }

    public void Update(){
        if(holding)
            SetPositionOfObject();
    }
}
