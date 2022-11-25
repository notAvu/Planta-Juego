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
    private string tagSuelo;

    #endregion

    #region Contructores

    void Awake()
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
        var velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        float direccionY = -Mathf.Sign(velocity.y);
        float direccionX = -Mathf.Sign(velocity.x);

        Vector2 posicionPlataforma = horizontal ?
            new Vector2(position.x + offset * direccionX, position.y) :
        new Vector2(position.x, position.y + offset * direccionY);

        GameObject plataformaCreada = Instantiate(plataformaPrefab, posicionPlataforma, Quaternion.identity);
        if (horizontal)
        {
            plataformaCreada.transform.Rotate(0, 0, 90f);
        }
        Destroy(this.gameObject);

    }
    private void PredictCollisionPoint(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(tagSuelo))
            {
                Vector3 collisionPoint = hit.ClosestPoint(position);
                float angle = Mathf.Abs(Vector3.Angle(position - collisionPoint, Vector2.right));
                bool horizontal = (int)angle == 0 || (int)angle == 180;

                CrearPlataforma(horizontal, hit.ClosestPoint(position));

            }
        }
    }
    #endregion
}

