using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHolder : MonoBehaviour
{
    [Header("In-Game data")]
    [SerializeField] GameObject currentObject = null;
    [SerializeField] Rigidbody currentBody = null;
    [SerializeField] float distanceFromCamera;
    [SerializeField] Vector3 mouseToWorld;

    [Header("Mouse Data")]
    [SerializeField] Vector2 mousePixels = Vector2.zero;
    [SerializeField] Vector2 mouseChange = Vector2.zero;
    [SerializeField] bool holding = false;
    [SerializeField] bool clicking = false;
    
    [Header("Camera Distance Data")]
    [SerializeField] float allowedZMin;
    [SerializeField] float allowedZMax;

    [Header("Other Requirements")]
    [SerializeField] RaycastManager raycastManager;
    [SerializeField] LayerMask holdableMask;
    [SerializeField] Camera cameraObject;
    [SerializeField] float forcePower;

    public void GetMousePos(InputAction.CallbackContext context){
        Vector2 newValue = context.ReadValue<Vector2>();
        mouseChange = newValue - mousePixels;
        mousePixels = newValue;
    }

    public void GetClick(InputAction.CallbackContext context){
        clicking = context.started || context.performed;

        if(context.started)
            GetSelectedObject();
        
        if(clicking == false && holding){
            currentBody.useGravity = true;
            currentObject = null;
            currentBody = null;
            holding = false;
        }
    }

    public void GetMiddleMouse(InputAction.CallbackContext context){
        int y = (int)context.ReadValue<Vector2>().y;
        distanceFromCamera = Mathf.Clamp(distanceFromCamera + y,allowedZMin,allowedZMax);
    }

    private Vector3 GetMousePosWorld(Vector2 input){
        Vector3 mouseVector3;

        if(currentObject == null)
            mouseVector3 = new Vector3(input.x,input.y,cameraObject.nearClipPlane);
        else{
            float diff = currentObject.transform.position.z - cameraObject.transform.position.z;
            mouseVector3 = new Vector3(input.x,input.y,diff);
        }
           
        return cameraObject.ScreenToWorldPoint(mouseVector3);
    }

    public void GetSelectedObject(){   
        RaycastHit[] value = null;
        currentObject = raycastManager.GetObjectFromCamera(mouseToWorld,out value,Mathf.Infinity,holdableMask);
        holding = currentObject != null;

        if(holding){
            currentBody = currentObject.GetComponent<Rigidbody>(); 
            currentBody.useGravity = false;
        }
    }

    private void SetPositionOfObject(){
        
        Vector2 directionV2 = ((Vector2)mouseToWorld - (Vector2)currentObject.transform.position);

        Vector2 direction = directionV2.normalized;
        Vector2 force = forcePower * direction;
        
        float distance = directionV2.magnitude;

        if(distance > 0.5f)
            currentBody.AddForce(new Vector3(force.x,force.y,0));
        
        currentBody.MovePosition(new Vector3(currentBody.position.x,currentBody.position.y,distanceFromCamera));
    }

    public void Update(){
        if(holding)
            SetPositionOfObject();

        mouseToWorld = GetMousePosWorld(mousePixels);
    }

    private void Awake(){
        distanceFromCamera = allowedZMin;
    }
}
