using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelector : NetworkBehaviour
{
    private bool canSelect;

    public GameObject messanger;

    public PlayerController playerToSpawn;

    public LevelSystem levelSystem;

    [SerializeField]
    private NetworkManager network;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSelect){
            if(Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = PlayerController.instance.transform.position;

                Destroy(PlayerController.instance.gameObject);

                PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                PlayerController.instance = newPlayer;

                gameObject.SetActive(false);


                CameraController.instance.target = newPlayer.transform;
                List<GameObject> goList = new List<GameObject>();
                goList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
                levelSystem.assignPlayer(goList[1].GetComponent<Player>());

                if (!isLocalPlayer)
                {
                    network = NetworkManager.FindObjectOfType<NetworkManager>();
                    network.playerPrefab = goList[1].GetComponent<Player>().gameObject;
                }
                //levelSystem.assignPlayer(newPlayer.transform.parent.GetComponent<Player>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            canSelect = true;
            messanger.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            canSelect = false;
            messanger.SetActive(false);
        }
    }
}
