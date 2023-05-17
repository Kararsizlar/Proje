using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectHolder : MonoBehaviour
{
    [Header("In-Game data, don't edit!")]
    [SerializeField] Vector3 mouseToWorld;
    public GameObject selectedObject;
    public ItemContainer currentHoldable = null;
    public Rigidbody currentBody = null;
    public bool holding = false;

    [Header("Mouse Data")]
    [SerializeField] Vector2 mousePixels = Vector2.zero;

    [Header("Pre-Game Data")]
    [SerializeField] RaycastManager raycastManager;
    [SerializeField] Camera cameraObject;
    [SerializeField] float forceMultiplier;
    [SerializeField] float yExtraHeight;
    [SerializeField] LayerMask ignoreHoldable;

    public void GetMousePos(InputAction.CallbackContext context){
        Vector2 newValue = context.ReadValue<Vector2>();
        mousePixels = newValue;
    }

    private void Hold(GameObject newObject){
        selectedObject = newObject;
        currentHoldable = selectedObject.GetComponent<ItemContainer>();
        currentBody = newObject.GetComponent<Rigidbody>();
        holding = true;

        currentBody.useGravity = false;
    }

    public void GetClick(InputAction.CallbackContext context){

        if(!context.started)
            return;

        GameObject selected = GetSelectedObject();
        if(selected == null){
            LetGoOfObject(false);
            return;
        }


        if(selected.layer == 6)//Holdable
        {
            if(holding)
                LetGoOfObject(true);

            Hold(selected);
            return;
        }

        if(selected.layer == 7)//Machine
        {
            Machine selectedMachine = selected.GetComponent<Machine>();
            if(holding)   
                selectedMachine.OnNewItem();
            else{
                GameObject outputObject = selectedMachine.GenerateOutput();
                if(outputObject != null)
                    Hold(outputObject);
            }
            return;
        }

        if(holding)
            LetGoOfObject(false);
    }

    public void LetGoOfObject(bool isReplacing){
        holding = isReplacing;

        if(currentBody != null)
            currentBody.useGravity = true;

        if(!holding){
            currentHoldable = null;
            currentBody = null;
            selectedObject = null;
        }
    }

    private Vector3 GetMousePosWorld(Vector2 input){
        RaycastHit hit;
        Ray ray = cameraObject.ScreenPointToRay(new Vector3(input.x,input.y,0));
        Vector3 start = cameraObject.transform.position;

        if(!holding)
            Physics.Raycast(start,ray.direction,out hit,Mathf.Infinity);
        else
            Physics.Raycast(start,ray.direction,out hit,Mathf.Infinity,ignoreHoldable);

        return hit.point;
    }

    public GameObject GetSelectedObject(){
        return raycastManager.GetObjectFromCamera(mouseToWorld,250,holding);
    }

    private void SetPositionOfObject(){
        Vector3 direction = (mouseToWorld - selectedObject.transform.position).normalized;
        float distance = (mouseToWorld - selectedObject.transform.position).magnitude;
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
