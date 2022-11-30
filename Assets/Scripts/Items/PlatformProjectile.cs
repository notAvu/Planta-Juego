using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectile : MonoBehaviour
{
    #region Atributos
    public GameObject platformPrefab;
    [SerializeField]
    private string groundTag;
    [SerializeField]
    private string playerTag;
    [SerializeField]
    private string enemyTag;

    #endregion

    #region unity Events
    void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag(playerTag).GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        SetIgnoreTag("Vines");
    }
    private void SetIgnoreTag(string tagToIgnore)
    {
        GameObject[] vines = GameObject.FindGameObjectsWithTag(tagToIgnore);
        foreach (GameObject vine in vines)
        {
            Physics2D.IgnoreCollision(vine.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }
    }

    #endregion
    private void FixedUpdate()
    {
        PredictCollisionPoint(transform.position, 0.2f);
    }

    #region methods
    /// <summary>
    /// Genera una plataforma en la posicion indicada
    /// </summary>
    /// <param name="vertical">indica si la plataforma se genera en vertical (como una columna) o en horizontal</param>
    /// <param name="position">Punto en el que se genera la plataforma</param>
    private void CreatePlatform(bool vertical, Vector3 position)
    {
        var velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        float yDirection = -Mathf.Sign(velocity.y);
        float xDirection = Mathf.Sign(velocity.x);

        Vector2 platformPosition = vertical ?
            new Vector2(position.x /*+ offset * xDirection*/, position.y) :
        new Vector2(position.x, position.y  /*+offset * yDirection*/);
        float platformRotation = xDirection < 0 ? 0 : 180;

        //GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);

        if (vertical)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            newPlatform.transform.Rotate(new Vector3(0, 0, platformRotation));
        }

    }

    /// <summary>
    /// Reconoce las colisiones con las que puede impactar el objeto en la posicion indicada en un radio determinado y genera una plataforma en la posicion de impacto
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
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(groundTag) && !hit.gameObject.name.Contains(platformPrefab.name))
            {
                Vector3 collisionPoint = hit.ClosestPoint(position); //pilo el punto de colision mas cercano desde la posicion del objeto 
                float angle = Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right)); // el angulo entre el vector que se forma desde la posicion al punto de
                                                                                                  // colision con respecto a el eje X (1,0)
                bool horizontal = (int)angle == 0 || (int)angle == 180;//Hago el cast a int pq el metodo de aproximar iba regu y la diferencia es de tan pocos decimales que asi
                                                                       //se aproxima correctamente. Si me da tiempo quiero buscar la forma de hacer una aptroximacion bien
                if (horizontal)// si pega en horizontal crea la plataforma desde el punto de colision calculado y se destruye la semilla
                {

                    CreatePlatform(horizontal, hit.ClosestPoint(position));
                }
                Destroy(gameObject);
            }
            else if (hit.CompareTag("Enemy") || (hit.CompareTag(groundTag) && (hit.gameObject.name.Contains(platformPrefab.name))))
            //todo: cambiar a que no se destruya con las tags que debe ignorar
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion
}

