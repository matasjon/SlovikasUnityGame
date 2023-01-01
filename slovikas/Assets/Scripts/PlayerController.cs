using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Vector2 moveInput;
    public Rigidbody2D theRB;
    public Transform gunArms;
    private Camera theCam;
    public Animator anim;
    public Player player;
    public GameObject bulletToFire;
    public Transform firepoint;
    public float timeBetweenShots;
    private  double shotCounter;

    public AudioSource FireballCast;
    public AudioSource FireArrow;

    private void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        if (bulletToFire.name == "fireblast")
        {
            GameObject fireBallGB = GameObject.Find("WizardFireballCast");
            FireballCast = fireBallGB.GetComponent<AudioSource>();
        }
        else if (bulletToFire.name == "ArrowType1")
        {
            GameObject fireBallGB = GameObject.Find("ArrowShot");
            FireballCast = fireBallGB.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(MainCanvasControl.isGamePaused != true) 
        {
            //Player movement
            PlayerMovement();

            //Player and gun rotation to mouse cursor
            PlayerAndGunRotation();

            //Movement animation
            MovementAnimation();

            // if(Input.GetMouseButtonDown(0)){
            //     Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
            //     shotCounter = timeBetweenShots;
            // }
            
            ShootingProjectiles();

        }
       
    }

    void PlayerMovement(){

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        theRB.velocity = moveInput * player.movementSpeed;
    }

    void PlayerAndGunRotation(){

        // mouse position
        Vector3  mousePos = Input.mousePosition;
        // player position
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
        
         //rotate player to mouse pointer position
        if(mousePos.x < screenPoint.x){

            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArms.localScale = new Vector3(-1f, -1f, 1f);
        }else{

            transform.localScale = new Vector3(1f, 1f, 1f);
            gunArms.localScale = new Vector3(1f, 1f, 1f);
        }

        //rotate gun arm/arms
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArms.rotation = Quaternion.Euler(0, 0, angle);
    }


    void MovementAnimation(){

        if(moveInput != Vector2.zero && moveInput.x > 0 || moveInput.x < 0){
            anim.SetBool("IsMovingUpDOWN", false);
            anim.SetBool("IsMovingSides", true);
        }
        else if(moveInput != Vector2.zero && moveInput.y > 0 || moveInput.y < 0 && moveInput.x == 0)
        {
            anim.SetBool("IsMovingSides", false);
            anim.SetBool("IsMovingUpDOWN", true);
        }

        else{
            anim.SetBool("IsMovingUpDOWN", false);
            anim.SetBool("IsMovingSides", false);
        }
    }

    void ShootingProjectiles(){

        if(Input.GetMouseButton(0) && Player.instance.mana >= 5){
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0){

                if (bulletToFire.name == "fireblast" || bulletToFire.name == "ArrowType1")
                {
                   FireballCast.Play();
                }

                Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
                Player.instance.TakeMana(2);
                shotCounter = player.attackSpeed;
            } 
        }
    }
}
