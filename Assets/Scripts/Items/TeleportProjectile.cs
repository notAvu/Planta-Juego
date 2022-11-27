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
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerYSizeOffset = (player.transform.localScale.y / 2) - 0.15f;
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

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
            bool dontDestroy = false;
            foreach (Collider2D hit in hits)
            {
                if (hit != null)
                {
                    Vector3 collisionPoint = hit.ClosestPoint(position);

                    Vector3 direction = transform.position - collisionPoint;

                    int angle = (int)Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                    //Debug.Log(angle);
                    //Debug.Log(direction.y);
                    bool horizontal = (angle == 0  || angle == 180 ) ;
                    //Debug.Log(horizontal);
                    dontDestroy =  hit.CompareTag(playerTag);
                    if (hit.CompareTag(groundTag) && !horizontal)
                    {
                        player.transform.position = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);

                        dontDestroy = false;
                    }

                    else if (hit.CompareTag("Vines") || hit.CompareTag("Untagged"))
                    {
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), hit);
                        dontDestroy = true;
                    }
                }
                Debug.Log(hit.tag);
            }
            if (!dontDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

}
