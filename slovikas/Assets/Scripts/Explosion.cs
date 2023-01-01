using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static Explosion instance;
    public float fieldOfImpact;
    public float force;

    public LayerMask layerToHit;
    // Start is called before the first frame update

    private void Awake(){

        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Explode();
                
        // }
    }

    public void Explode()
    {
        // Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

        // foreach(Collider2D obj in objects)
        // {

        //     Vector2 direction = obj.transform.position - transform.position;
        //     obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        // }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,fieldOfImpact);

    }
}
