using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    #region referencias de escena
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform gunHolder;
    [SerializeField]
    private Transform gunPivot;
    #endregion
    #region 
    [SerializeField]
    private KeyCode launchButton;
    #endregion
    #region
    #region Can this be translated to the projectile's script?
    [SerializeField]
    private GameObject projectilePrefab;

    private Rigidbody2D projectileRb;
    private float projectileMass;
    private LineRenderer lineRenderer;
    #endregion
    [SerializeField]
    private float launchForce; //Valor de ejemplo 500
    private Vector3 distanceVector;
    #endregion
    #region eventos de Unity
    private void Start()
    {
        projectileRb = projectilePrefab.GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        projectileMass = projectilePrefab.gameObject.GetComponent<Rigidbody2D>().mass;
    }
    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(launchButton))
        {
            lineRenderer.enabled = true;
            DrawProjectileTrajectory();
        }
        if (Input.GetKeyUp(launchButton))
        {
            lineRenderer.enabled = false;
            LaunchProjectile();
        }
        RotateGun(mousePosition);
    }
    #endregion
    #region Metodos auxiliares
    /// <summary>
    /// Rota el arma/canon/tirachinas para que la punta mire al puntero del raton
    /// </summary>
    /// <param name="targetPoint">el vector posicion del raton</param>
    private void RotateGun(Vector3 targetPoint)
    {
        distanceVector = targetPoint - gunPivot.position;
        float angleDeg = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
    }
    /// <summary>
    /// Instancia un proyectiil desde la posicion del eje de rotacion del lanzador y lo dispara aplicandole una fuerza igual a <see cref="launchForce"/>
    /// </summary>
    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        Vector2 direction = distanceVector.normalized;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * launchForce);

        //Debug.Log($"{direction.x}x {direction.y}y");
    }
    #endregion
    #region Dibujar trayectoria
    private void DrawProjectileTrajectory()
    {
        //List<Vector2> linePoints = new List<Vector2>();
        float simulationDuration = 3f;
        float stepInterval = 0.01f;
        int steps = (int)(simulationDuration / stepInterval);
        lineRenderer.positionCount = steps;
        float velocity = (launchForce / projectileMass) * Time.fixedDeltaTime;
        float drag = Mathf.Pow(velocity, 2) * projectileRb.drag;
        //velocity = velocity * (1 - Time.fixedDeltaTime * drag);
        Debug.Log(projectileMass);
        Vector3 nextposition;
        for (int i = 0; i < steps; i++)
        {
            nextposition = gameObject.transform.position + distanceVector.normalized * velocity * stepInterval * i;
            float gravity = Physics2D.gravity.y / 2 * Mathf.Pow(i * stepInterval, 2);
            nextposition.y += gravity;
            lineRenderer.SetPosition(i, nextposition);
            if (LineCollided(nextposition))
            {
                steps = i;
                lineRenderer.positionCount = steps;
            }
        }
        //return linePoints;
    }
    private bool LineCollided(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, 0.1f);

        return hits.Length>0;
    }
    #endregion
}
