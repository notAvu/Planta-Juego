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
        //GetComponent<Rigidbody2D>().
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
            foreach (Collider2D hit in hits)
            {
                if (hit != null)
                {
                    Vector3 collisionPoint = hit.ClosestPoint(position);

                    Vector3 direction = transform.position - collisionPoint;

                    int angle = (int)Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                    //Debug.Log(direction.y);
                    bool horizontal = (angle == 0 || angle == 180) && direction.magnitude>0.0001;
                    //Debug.Log(direction.magnitude);
                    if (hit.CompareTag(groundTag))
                    {
                        Debug.Log(angle+hit.tag);
                        if (!horizontal)
                            player.transform.position = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);

                        Destroy(gameObject);
                    }
                }
                //Debug.Log(hit.tag);
            }
            //Destroy(gameObject);
        }
    }

}
