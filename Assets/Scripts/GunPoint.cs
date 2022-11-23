using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    //estas variables se asignan asumiendo que ambas semillas tienen las mismas propiedades fisicas para el lanzamiento
    [SerializeField]
    private float launchForce = 15f;


    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Transform gunHolder;
    [SerializeField]
    private Transform gunPivot;

    Vector3 distanceVector;
    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Z))
            LaunchProjectile();
        RotateGun(mousePosition);
    }
    /// <summary>
    /// Rota el arma/canon/tirachinas para que la punta mire al puntero del raton
    /// </summary>
    /// <param name="targetPoint">el vector posicion del raton</param>
    private void RotateGun(Vector3 targetPoint)
    {
        distanceVector = targetPoint - gunPivot.position;
        Debug.Log(distanceVector.x);
        float angleDeg = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        Vector2 direction = distanceVector.normalized;
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * launchForce);
        Debug.Log($"{direction.x}x {direction.y}y");

    }
}
