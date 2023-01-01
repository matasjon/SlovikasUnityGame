using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructables : MonoBehaviour
{
    Animator animator;
    public double health = 50;

    public BoxCollider2D collider;
     public LayerMask layerToHit;


    public AudioSource explosionSound;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Weapon")
        {
            health -= Player.instance.damage;
            if(health > 0){
                animator.SetBool("IsDamaged", true);


                if (GetComponent<Rigidbody2D>() != null)
                {
                    Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
                    rigidbody2d.constraints = RigidbodyConstraints2D.None;
                    rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                    StartCoroutine(SetConstraints());
                }
                StartCoroutine(WaitForAnimation());
            }else
            {

                if (explosionSound != null)
                {
                    explosionSound.Play();
                    
                }
                animator.SetBool("IsDestroyed", true);
                collider.enabled = false;

                if(gameObject.tag == "Barrel" ){

                    if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 3){
                         Player.instance.TakeDamage(20);
                         Vector2 direction = PlayerController.instance.transform.position - transform.position;
                         PlayerController.instance.GetComponent<Rigidbody2D>().AddForce(direction * 500,  ForceMode2D.Force);

                    }
                         
                    // Explosion.instance.Explode();

                    // Vector2 direction = PlayerController.instance.transform.position - transform.position;
                    // PlayerController.instance.GetComponent<Rigidbody2D>().AddForce(direction * 1000,  ForceMode2D.Force);


                }

                if (GetComponent<Rigidbody2D>() != null)
                {
                    Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
                    rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
                }


            }
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.05F);
        animator.SetBool("IsDamaged", false);
    }

    private IEnumerator SetConstraints()
    {

        yield return new WaitForSeconds(0.5f);

        if (GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezePosition;
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }
}

