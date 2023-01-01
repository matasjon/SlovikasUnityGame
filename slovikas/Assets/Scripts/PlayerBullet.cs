using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public GameObject impactEffect;

    public AudioSource impactSound;
    public AudioSource shotSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(theRB.name);
        if (theRB.name == "ArrowType1(Clone)")
        {
            GameObject impactObject = GameObject.Find("ArrowHit");
            impactSound = impactObject.GetComponent<AudioSource>();
            GameObject shotSoundGameObject = GameObject.Find("ArrowShot");
            shotSound = shotSoundGameObject.GetComponent<AudioSource>();

            GetComponent<AudioSource>().Play();
        }
        else if(theRB.name == "fireblast(Clone)")
        {
            
            GameObject impactObject = GameObject.Find("FireBallHit");
            impactSound = impactObject.GetComponent<AudioSource>();
            Debug.Log(impactSound);
            GameObject shotSoundGameObject = GameObject.Find("WizardFireballCast");
            shotSound = shotSoundGameObject.GetComponent<AudioSource>();
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag != "HealthPotion" && other.tag != "ManaPotion" && other.tag != "Gold1"){

            StartCoroutine(WaitForAnimation());
            GetComponent<AudioSource>().Stop();
            Instantiate(impactEffect, transform.position, transform.rotation);
            impactSound.Play();
        }
    }

    private void OnBecameInvisible(){
        Destroy(gameObject);
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
