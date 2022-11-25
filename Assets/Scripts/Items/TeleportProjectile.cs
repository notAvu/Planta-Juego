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
        offset = player.transform.localScale.y / 2;
    }
    private void FixedUpdate()
    {
        PredictCollisionPoint(gameObject.transform.position, 0.2f);
    }
    private void PredictCollisionPoint(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);
        if (hits.Length > 1)
        {
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag(groundTag))
                {
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + offset);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
