using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed;

    private Vector3 direction;

    public int projectileDamage = 5;


    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player"){
            
            Player.instance.TakeDamage(projectileDamage);
        }
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible(){
        Destroy(gameObject);
    }
}
