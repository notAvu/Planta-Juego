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
        Physics2D.queriesHitTriggers = true;
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerYSizeOffset = player.transform.localScale.y / 2;
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
                Vector3 collisionPoint = hit.ClosestPoint(position);

                Vector3 direction = transform.position - collisionPoint;

                float angle = Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                
                bool horizontal = ((int)angle == 0 || (int)angle == 180) && direction.y<=0;
                
                dontDestroy = hit.CompareTag("Trigger") || hit.CompareTag("Player");
                if (hit.CompareTag(groundTag) && !horizontal )
                {
                    var newPosition = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);
                    player.transform.position = newPosition;

                    dontDestroy = false;
                }
                else if (hit.CompareTag("Trigger"))
                {
                    CameraChangerController cameraTrigger;
                    hit.gameObject.TryGetComponent(out cameraTrigger);
                    cameraTrigger.CambiaPosicionCamara();
                    dontDestroy = true;
                }
            }
            if (!dontDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
    private void CheckCollisionsOnTeleport(Vector2 newPosition)
    {
        var playerHit = Physics2D.BoxCast(new Vector2(newPosition.x, newPosition.y + playerYSizeOffset), player.GetComponent<BoxCollider2D>().size, 0f, Vector2.up);
        if (!playerHit)
        {

            //Debug.Log(playerHit.point);
        }
    }

}
