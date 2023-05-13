using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemMover : MonoBehaviour
{
    public IEnumerator IMoveUI(RectTransform rectTransform,Vector3 target,float timeToTween){
        float delta = 0;
        Vector3 startPos = rectTransform.localPosition;
        while (delta < timeToTween)
        {
            yield return Time.deltaTime;
            delta += Time.deltaTime;
            float t = Mathf.Sin(Mathf.Deg2Rad * (delta / timeToTween * 90));
            rectTransform.localPosition = Vector3.Lerp(startPos,target,t);
        }
    }
}
