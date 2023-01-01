using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField]
    public Transform target;

    [SerializeField]
    private Vector3 offsetPosition = new Vector3(0f, 0f, -10f);

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Awake(){
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (target == null)
            return;
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }
    }
    
}
