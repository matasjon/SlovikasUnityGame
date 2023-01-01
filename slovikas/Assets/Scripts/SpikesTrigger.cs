using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]
public class SpikesTrigger : MonoBehaviour
{
    [SerializeField]
    private string spikesTag = "Spikes";

    [SerializeField, Min(0f)]
    private int damage = 30;

    private Rigidbody2D rb;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (IsSpikes(otherGameObject))
        {
            player.TakeDamage(damage);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        if (IsSpikes(otherGameObject))
        {
            player.TakeDamage(damage);
        }
    }

    private bool IsSpikes(GameObject obj)
    {
        return obj.CompareTag(spikesTag);
    }


}
