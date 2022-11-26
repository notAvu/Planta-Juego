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
            bool lateDestroy = false;
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
                    lateDestroy = hit.CompareTag("Trigger") || hit.CompareTag("Player");
                    if (hit.CompareTag(groundTag) && !horizontal)
                    {
                        player.transform.position = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);
                        
                        lateDestroy = false;
                    }
                    else if (hit.CompareTag("Trigger"))
                    {
                        CameraChangerController cameraTrigger;
                        hit.gameObject.TryGetComponent(out cameraTrigger);
                        if (cameraTrigger != null)
                            cameraTrigger.CambiaPosicionCamara();
                        lateDestroy = true;
                    }
                    //teleport = !hit.CompareTag("Vines") && (hit.CompareTag(groundTag) && !horizontal)
                }
            }
            //if(teleport)
            //{
            //  var newPosition = player.transform.position = newPosition;
            //  lateDestroy = false;
            //}
            if (!lateDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(nameof(LateDestroy));
            }
        }
    }

    private IEnumerator LateDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        //He puesto el late destroy con un limitador de tiempo para que si la semilla cae demasiado lejos fuera de camara
        //no teletransporte al jugador a, por ejemplo, una caida o un sitio donde se quede estancado.
        //Aun asi el jugador puede caer al otro lado del trigger de la camara y teleportarse, se puede ajustarse el tiempo segun se quiera para asegurarse 
        //de que el teletransporte del jugador fuera de la camara funcione como queramos 
        Destroy(gameObject);
    }

}
