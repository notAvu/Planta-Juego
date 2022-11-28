using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProjectile : MonoBehaviour
{
    [SerializeField]
    private string playerTag;
    [SerializeField]
    private string groundTag;
    [SerializeField]
    private string enemyTag;
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
    /// <summary>
    /// Reconoce las colisiones con las que puede impactar el objeto en la posicion indicada en un radio determinado
    /// </summary>
    /// <remarks>
    /// Utilizo este metodo en lugar del on collision porque me permite comprobar los datos de la colision antes de que ocurra.
    /// De lo contrario la colision podria o no haber cambiado la trayectoria del proyectil y el calculo del angulo y la direccion no funcionarian
    /// </remarks>
    /// <param name="position">la posicion actual del objeto</param>
    /// <param name="stepSize">el radio de deteccion de colisiones</param>
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
                    Vector3 collisionVector = transform.position - collisionPoint;

                    int angle = (int)Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                    bool horizontalCollision = (angle == 0 || angle == 180) && collisionVector.magnitude>0.0001;
                    if (hit.CompareTag(groundTag) )
                    {
                        if (!horizontalCollision)
                            player.transform.position = new Vector2(transform.position.x, transform.position.y + playerYSizeOffset);

                        Destroy(gameObject);
                    }
                    else if(hit.CompareTag(enemyTag))
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

}
