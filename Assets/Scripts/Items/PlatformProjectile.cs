using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectile : MonoBehaviour
{
    //Se asociaria al prefab de la Semilla que creará la plataforma
    #region Atributos
    public GameObject plataformaPrefab; 
    private float offset;
    [SerializeField]
    private string tagPared;
    [SerializeField]
    private string tagSuelo;//Creo las dos tags para diferenciar cuando debe instanciarse en horizontal o en vertical, en funcion de donde impacte la semilla
    #endregion

    #region Contructores

    void Start()
    {
        offset = plataformaPrefab.transform.localScale.y / 2;
    }

    #endregion
    private void FixedUpdate()
    {
        PredictCollisionPoint(transform.position, 0.2f);
    }

    #region Metodo Privado

    private void CrearPlataforma(bool horizontal, Vector3 position)
    {
        float direccionY = -Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.y);
        float direccionX = -Mathf.Sign(gameObject.GetComponent<Rigidbody2D>().velocity.x);

        Vector2 posicionPlataforma = horizontal ?
            new Vector2(position.x + offset * direccionX, position.y) :
        new Vector2(position.x, position.y + offset * direccionY);
        GameObject plataformaCreada;

        plataformaCreada = Instantiate(plataformaPrefab, posicionPlataforma, Quaternion.identity);
        if (horizontal)
            plataformaCreada.transform.Rotate(0, 0, 90f);
        plataformaCreada.GetComponent<Platform>().StartDestructionTimer();
        Destroy(this.gameObject);

    }
    private void PredictCollisionPoint(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(tagSuelo))
            {
                CrearPlataforma(false, hit.ClosestPoint(position));
            }
            else if (hit.CompareTag(tagPared))
            {
                CrearPlataforma(true, hit.ClosestPoint(position));
            }
        }
    }

    #endregion
}

