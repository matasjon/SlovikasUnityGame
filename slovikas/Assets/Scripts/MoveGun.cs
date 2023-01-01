using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGun : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D theRB;

    public Transform gunArms;
    private Camera theCam;



    // Start is called before the first frame update
    void Start()
    {
       // theCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //  transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime* moveSpeed, 0f);

        theRB.velocity = moveInput * moveSpeed;

        // mouse position
        Vector3 mousePos = Input.mousePosition;
        // player position
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        //rotate player to mouse pointer position
        if (mousePos.x < screenPoint.x)
        {

            transform.localScale = new Vector3(1f, 1f, 1f);
            gunArms.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {

            transform.localScale = Vector3.one;
            gunArms.localScale = Vector3.one;
        }

      

        // movement

      

    }
}
