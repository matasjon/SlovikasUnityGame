using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]
public class RoomClose : MonoBehaviour
{

    private string colliderTag = "EntranceTrigger";

    [SerializeField]
    private string doorTag = "Door";
    private string doorObjectTag = "DoorObject";
    private string doorSpriteTag = "DoorSprite";
    private string enemySpawnTag = "enemySpawn";
    private string spawnEnemyTag = "spawnEnemy";

    public GameObject enemy;
     public GameObject enemy2;
    public int enemiesToKill = 4;
    private Rigidbody2D rb;
    private Player player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (IsEntrance(otherGameObject))
        {
            Destroy(otherGameObject.transform.parent.gameObject);
            List<GameObject> goList = new List<GameObject>();
            goList = new List<GameObject>(GameObject.FindGameObjectsWithTag(doorTag));
            if (goList.Count > 0)
            {
                for (int i = 0; i < goList.Count; i++)
                {
                    GameObject go = goList[i];
                    Transform[] ts = go.GetComponentsInChildren<Transform>(true);
                    foreach (Transform o in ts)
                    {
                        o.gameObject.active = true;
                        if (IsDoorObject(o.gameObject))
                        {
                            Transform[] ts2 = o.gameObject.GetComponentsInChildren<Transform>(true);
                            foreach (Transform o2 in ts2)
                            {
                                if (IsDoorSprite(o2.gameObject))
                                {
                                    StartCoroutine(MakeVisible(o2.gameObject));
                                }
                            }
                        }
                        else if (IsEnemySpawn(o.gameObject))
                        {
                            Transform[] ts2 = o.gameObject.GetComponentsInChildren<Transform>(true);
                            foreach (Transform o2 in ts2)
                            {

                                int drop = Random.Range(0, 100);

                                if(drop < 60){
                                    Instantiate(enemy, o2.gameObject.transform.position, enemy.transform.rotation);
                                }else{
                                    Instantiate(enemy2, o2.gameObject.transform.position, enemy2.transform.rotation);
                                }

                                Destroy(o2.gameObject);
                            }
                        }

                    }
                }
            }
        }


    }

    private IEnumerator MakeVisible(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    private IEnumerator MakeInvisible(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.05F);
        obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);
        obj.transform.parent.gameObject.active = false;
    }

    private bool IsEntrance(GameObject obj)
    {
        return obj.CompareTag(colliderTag);
    }
    private bool IsDoorSprite(GameObject obj)
    {
        return obj.CompareTag(doorSpriteTag);
    }
    private bool IsDoorObject(GameObject obj)
    {
        return obj.CompareTag(doorObjectTag);
    }
    private bool IsEnemySpawn(GameObject obj)
    {
        return obj.CompareTag(enemySpawnTag);
    }
    private bool IsSpawnEnemy(GameObject obj)
    {
        return obj.CompareTag(spawnEnemyTag);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals) || enemiesToKill == 0)
        {
            enemiesToKill = 4;
            List<GameObject> goList = new List<GameObject>();
            goList = new List<GameObject>(GameObject.FindGameObjectsWithTag(doorTag));
            if (goList.Count > 0)
            {
                for (int i = 0; i < goList.Count; i++)
                {
                    GameObject go = goList[i];
                    Transform[] ts = go.GetComponentsInChildren<Transform>(true);
                    foreach (Transform o in ts)
                    {
                        if (IsDoorObject(o.gameObject))
                        {
                            Transform[] ts2 = o.gameObject.GetComponentsInChildren<Transform>();
                            foreach (Transform o2 in ts2)
                            {
                                if (IsDoorSprite(o2.gameObject))
                                {
                                    StartCoroutine(MakeInvisible(o2.gameObject));
                                }
                            }
                        }
                    }
                }
            }

        }

    }
}
