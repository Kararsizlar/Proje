using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHolder : MonoBehaviour
{
    [Header("In-Game data, don't edit!")]
    [SerializeField] Vector3 mouseToWorld;
    public Holdable currentObject = null;
    public Rigidbody currentBody = null;
    public bool holding = false;

    [Header("Mouse Data")]
    [SerializeField] Vector2 mousePixels = Vector2.zero;
    [SerializeField] Vector2 mouseChange = Vector2.zero;

    [Header("Pre-Game Data")]
    [SerializeField] RaycastManager raycastManager;
    [SerializeField] LayerMask holdableMask,ignoreHoldable;
    [SerializeField] Camera cameraObject;
    [SerializeField] float forceMultiplier;
    [SerializeField] float yExtraHeight;

    public void GetMousePos(InputAction.CallbackContext context){
        Vector2 newValue = context.ReadValue<Vector2>();
        mouseChange = newValue - mousePixels;
        mousePixels = newValue;
    }

    public void GetClick(InputAction.CallbackContext context){
        if(context.started && holding == false)
            GetSelectedObject();
        else if(context.started && holding == true)
            StartCoroutine(LetGoOfObject());
    }

    private IEnumerator LetGoOfObject(){
        yield return new WaitForEndOfFrame();
        holding = false;  
        
        if(currentBody == null)
            yield break;
        
        currentBody.useGravity = true;
        currentObject = null;
        currentBody = null;
    }

    private Vector3 GetMousePosWorld(Vector2 input){
        
        Vector3 GetRegularMousePos(){
            Vector3 v = new Vector3(input.x,input.y,cameraObject.nearClipPlane);
            return cameraObject.ScreenToWorldPoint(v);
        }

        Vector3 GetTargetPosForHolding(){
            RaycastHit hitInfo = new RaycastHit();
            Vector3 camPos = cameraObject.transform.position;
            Vector3 direction = GetRegularMousePos() - camPos;
            Physics.Raycast(camPos,direction,out hitInfo,Mathf.Infinity,ignoreHoldable);

            Vector3 hitPos = hitInfo.point;
            Transform hitTransform = hitInfo.transform;
                
            return new Vector3(hitPos.x,hitInfo.transform.position.y + yExtraHeight,hitPos.z);
        }

        Vector3 mouseVector3;
        if(holding){
            mouseVector3 = GetTargetPosForHolding();
            return mouseVector3;
        }
        else
            return GetRegularMousePos();
    }

    public void GetSelectedObject(){   
        RaycastHit[] value = null;
        GameObject current = raycastManager.GetObjectFromCamera(mouseToWorld,out value,Mathf.Infinity,holdableMask);

        if(current == null)
            return;

        currentObject = current.GetComponent<Holdable>();
        holding = currentObject != null;

        if(holding){
            currentBody = currentObject.GetComponent<Rigidbody>(); 
            currentBody.useGravity = false;
        }
    }

    private void SetPositionOfObject(){
        Vector3 direction = (mouseToWorld - currentObject.transform.position).normalized;
        float distance = (mouseToWorld - currentObject.transform.position).magnitude;
        Vector3 force = direction * forceMultiplier;
 
        currentBody.AddForce(new Vector3(force.x,force.y,force.z) * distance);
    }

    public  void FixedUpdate(){
        if(holding)
            SetPositionOfObject();
    }

    public void Update(){
        mouseToWorld = GetMousePosWorld(mousePixels);
    }
}
