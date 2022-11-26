using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectile : MonoBehaviour
{
    //Se asociaria al prefab de la Semilla que creará la plataforma
    #region Atributos
    public GameObject platformPrefab;
    private float offset;
    [SerializeField]
    private string groundTag;
    [SerializeField]
    private string playerTag;

    #endregion

    #region Contructores

    void Awake()
    {
        Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag(playerTag).GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        offset = platformPrefab.transform.localScale.y / 2;
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
        float yDirection = -Mathf.Sign(velocity.y);
        float xDirection = -Mathf.Sign(velocity.x);

        Vector2 platformPosition = horizontal ?
            new Vector2(position.x + offset * xDirection, position.y) :
        new Vector2(position.x, position.y + offset * yDirection);

        GameObject newPlatform = Instantiate(platformPrefab, platformPosition, Quaternion.identity);
        if (horizontal)
        {
            newPlatform.transform.Rotate(0, 0, 90f);
        }
        Destroy(gameObject);

    }
    private void PredictCollisionPoint(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(groundTag) && !hit.gameObject.name.Contains(platformPrefab.name))
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

