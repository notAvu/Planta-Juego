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
    float playerYSizeOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerYSizeOffset = player.transform.localScale.y / 2;
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
                Vector3 collisionPoint = hit.ClosestPoint(position);
                float angle = Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                bool horizontal = (int)angle == 0 || (int)angle == 180;
                if (hit.CompareTag(groundTag) && !horizontal)
                {
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);
                }
            }
            Destroy(gameObject);
        }
    }
}
