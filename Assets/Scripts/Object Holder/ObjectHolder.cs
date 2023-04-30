using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHolder : MonoBehaviour
{
    [Header("In-Game data, don't edit!")]
    [SerializeField] float distanceFromCamera;
    [SerializeField] Vector3 mouseToWorld;
    public Holdable currentObject = null;
    public Rigidbody currentBody = null;
    public bool holding = false;

    [Header("Mouse Data")]
    [SerializeField] Vector2 mousePixels = Vector2.zero;
    [SerializeField] Vector2 mouseChange = Vector2.zero;
    [SerializeField] bool clicking = false;

    [Header("Camera Distance Data")]
    [SerializeField] float allowedZMin;
    [SerializeField] float allowedZMax;

    [Header("Requirements")]
    [SerializeField] RaycastManager raycastManager;
    [SerializeField] LayerMask holdableMask;
    [SerializeField] Camera cameraObject;
    [SerializeField] float forceMultiplier;
    [SerializeField] float zAxisMultiplier;

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
            holding = false;

            currentBody.useGravity = true;
            currentObject = null;
            currentBody = null;
        }
    }

    public void GetMiddleMouse(InputAction.CallbackContext context){
        float y = System.Math.Sign(context.ReadValue<Vector2>().y) * zAxisMultiplier;
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
        GameObject current = raycastManager.GetObjectFromCamera(mouseToWorld,out value,Mathf.Infinity,holdableMask);
        
        if(current == null)
            return;

        current.TryGetComponent<Holdable>(out currentObject);
        holding = currentObject != null;

        if(holding){
            currentBody = currentObject.GetComponent<Rigidbody>(); 
            currentBody.useGravity = false;
        }
    }

    private void SetPositionOfObject(){
        
        Vector2 directionV2 = ((Vector2)mouseToWorld - (Vector2)currentObject.transform.position);

        Vector2 direction = directionV2.normalized;
        Vector2 force = Vector2.zero;
        
        float distance = directionV2.magnitude;
        force = direction * (distance * forceMultiplier);

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
