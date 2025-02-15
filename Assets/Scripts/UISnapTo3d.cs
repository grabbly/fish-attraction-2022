using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISnapTo3d : MonoBehaviour
{
    // [SerializeField] private Vector3 offset;
    
    public void Snap(Transform worldTarget)
    {
        var screenPoint = Game.instance.GameCamera.WorldToScreenPoint(worldTarget.position);
        screenPoint.z = transform.position.z;
        var uiPos = UIController.instance.uiCamera.ScreenToWorldPoint(screenPoint);// + offset;
        uiPos.z = transform.position.z;
        transform.position = uiPos;
    }
}
