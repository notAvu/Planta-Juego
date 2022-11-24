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
    #region input disparo
    //[SerializeField]
    //private KeyCode launchButton;
    //[SerializeField]
    //private KeyCode switchProjectileButton;

    private float projectileOneAxisVal;
    private float projectileTwoAxisVal;
    #endregion
    #region
    #region variables prefabs proyectiles
    [SerializeField]
    private GameObject[] availablePrefabs;
    
    private GameObject activeProjectile;

    private bool chargingProjectile;
    private int selectedPrefabIndex;

    private LineRenderer lineRenderer;
    #endregion
    [SerializeField]
    private float launchForce; //Valor de ejemplo 500
    private Vector3 distanceVector;
    #endregion
    #region eventos de Unity
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        selectedPrefabIndex = 0;
        chargingProjectile = false;
        activeProjectile = null;
    }
    private void Update()
    {
        projectileOneAxisVal = Input.GetAxisRaw("Fire1");
        projectileTwoAxisVal = Input.GetAxisRaw("Fire2");
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //if (Input.GetKey(launchButton))
        if (projectileOneAxisVal > 0 && activeProjectile == null)
        {
            SelectProjectileByIndex(0);
            ChargeProjectile();
        }
        else if (projectileTwoAxisVal > 0 && activeProjectile == null)
        {
            SelectProjectileByIndex(1);
            ChargeProjectile();
        }
        //if (Input.GetKeyUp(launchButton))
        else if((projectileOneAxisVal<=0|| projectileTwoAxisVal <= 0) && chargingProjectile)
        {
            LaunchProjectile();
        }
        //if (Input.GetKeyDown(switchProjectileButton))
        //{
        //    SwitchProjectile();
        //}
        lineRenderer.enabled = chargingProjectile;
        RotateGun(mousePosition);
    }

    private void ChargeProjectile()
    {
        chargingProjectile = true;
        DrawProjectileTrajectory();
    }
    #endregion
    #region Metodos de lanzamiento
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
        activeProjectile = Instantiate(availablePrefabs[selectedPrefabIndex], gameObject.transform.position, Quaternion.identity);
        Vector2 direction = distanceVector.normalized;
        activeProjectile.GetComponent<Rigidbody2D>().AddForce(direction * launchForce);
        chargingProjectile = false;
    }
    #endregion
    #region Dibujar trayectoria
    /// <summary>
    /// Simula la trayectoria del proyectil mostrando la trayectoria que va a seguir con el line renderer
    /// </summary>
    private void DrawProjectileTrajectory()
    {
        float simulationDuration = 3f;
        float stepInterval = 0.05f;
        int steps = (int)(simulationDuration / stepInterval);
        lineRenderer.positionCount = steps;
        float projectileMass = availablePrefabs[selectedPrefabIndex].gameObject.GetComponent<Rigidbody2D>().mass;
        float velocity = (launchForce / projectileMass) * Time.fixedDeltaTime;
        Vector3 nextposition;
        for (int i = 0; i < steps; i++)
        {
            nextposition = gameObject.transform.position + distanceVector.normalized * velocity * stepInterval * i;
            float gravity = Physics2D.gravity.y / 2 * Mathf.Pow(i * stepInterval, 2);
            nextposition.y += gravity;
            lineRenderer.SetPosition(i, nextposition);
            if (LineCollided(nextposition, stepInterval))
            {
                steps = i;
                lineRenderer.positionCount = steps;
            }
        }
    }
    /// <summary>
    /// Metodo que calcula si en el siguiente paso de la linea de trayectoria colisionaria
    /// </summary>
    /// <param name="position">la posicion del siguiente punto</param>
    /// <param name="stepSize">el radio del area que detecta colisiones</param>
    /// <returns>true si la linea tocaria un collider en el siguiente punto de la trayectoria</returns>
    private bool LineCollided(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);

        return hits.Length>0;
    }
    #endregion
    #region seleccionar proyectil
    /// <summary>
    /// Cambia el proyectil a lanzar al siguiente del array de pryectiles
    /// </summary>
    private void SwitchProjectile()
    {
        selectedPrefabIndex++;
        if(selectedPrefabIndex >= availablePrefabs.Length)
        {
            selectedPrefabIndex = 0;
        }
    }
    /// <summary>
    /// Cambia el proyectil seleccionado en funcion del indice indicado
    /// </summary>
    /// <param name="index"></param>
    private void SelectProjectileByIndex(int index)
    {
        if (index < availablePrefabs.Length)
        {
            selectedPrefabIndex = index;
        }
    }
    #endregion
}
