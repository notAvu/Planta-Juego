using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProjectile : MonoBehaviour
{
    [SerializeField]
    private string playerTag;
    [SerializeField]
    private string groundTag;
    private GameObject player;
    float offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        offset = player.transform.localScale.y/2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            player.transform.position =
            new Vector2(transform.position.x, transform.position.y + offset);
            
        }
        Destroy(gameObject);
    }
}
