using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public static EnemyController instance;

    public Rigidbody2D theRB;
    public float movementSpeed;

    public float rangeToChase;
    private Vector3 moveDirection; 

    public Animator anim;

    public int health = 100;

    public GameObject[] deathSplatters;

    public GameObject hitEffect;
    public RoomClose roomClose;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firepoint;

    public float fireRate;
    private float fireCounter;

    public SpriteRenderer theBody;

    public float shootRange;

    public int attackDamage;


    private void Awake(){

        instance = this;
    }

    void Start()
    {
        roomClose = GameObject.FindGameObjectWithTag("Player").GetComponent<RoomClose>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible)
        {
            // jei bus 2 zaideju rezimas tai cia dar viena if idet kad du instance is karto ziuretu
            if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase){

                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else{
                moveDirection = Vector3.zero;
            }
            
            moveDirection.Normalize();
            theRB.velocity = moveDirection * movementSpeed;


            // rotation
            Vector3  playerPos = PlayerController.instance.transform.position;
            Vector3  enemyPos = transform.position;

            if(playerPos.x < enemyPos.x){
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }else{
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            /// shooting
            
            if(shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange){

                fireCounter -= Time.deltaTime;

                if(fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firepoint.transform.position, firepoint.transform.rotation);
                }
            }
        }


        //movement animation

        if(moveDirection != Vector3.zero){

            anim.SetBool("isMoving", true);

        }else{
            anim.SetBool("isMoving", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Weapon")
        {
            health -= Player.instance.damage;

            Instantiate(hitEffect, transform.position, transform.rotation);

            if(health < 0){
                roomClose.enemiesToKill--;
                Destroy(gameObject);
                LevelSystem.instance.LevelUpgrade(true);

                int selectedSplatter = Random.Range(3, 5);

                int rotation = Random.Range(0, 4);

                Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

                if(Random.value < 0.5f){

                    int drop = Random.Range(0, 3);
                    Instantiate(deathSplatters[drop], transform.position, transform.rotation);
                }
                
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other){

        if(other.collider is CircleCollider2D && other.collider.tag == "Player"){

            Player.instance.TakeDamage(attackDamage);
        }
    }


}
