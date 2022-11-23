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

    [SerializeField]
    private GameObject projectilePrefab;
    private float projectileMass;
    [SerializeField]
    private float launchForce; //Valor de ejemplo 500
    private Vector3 distanceVector;
    #endregion
    #region eventos de Unity
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(launchButton)) { 
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

        projectileMass = projectile.GetComponent<Rigidbody2D>().mass;
        //Debug.Log($"{direction.x}x {direction.y}y");
    }
    #endregion
    #region dibujar trayectoria
    private void DrawProjectileTrajectory()
    {
        List<Vector2> linePoints = new List<Vector2>();
        int steps = 20;
        float stepInterval = 0.1f;
        float velocity = (launchForce / projectileMass) + Time.fixedDeltaTime;
        Vector2 nextposition; 
        for(int i = 0; i< steps; i++)
        {
            nextposition = gameObject.transform.position + distanceVector.normalized * velocity * stepInterval * i; 
            //nextposition = Physics2D.gravity.y
        }
    }
    #endregion
}
