using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField]
    private CameraController camera;
    // Start is called before the first frame update
    void Start()
    {
        // if (!isLocalPlayer)
        // {

        // }

        camera = CameraController.FindObjectOfType<CameraController>();
        camera.target = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}