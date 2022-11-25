using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformProjectile : MonoBehaviour
{
    //Se asociaria al prefab de la Semilla que creará la plataforma
    #region Atributos

    //booleano para comprobar si hay o no plataforma creada
    private bool creado;
    private GameObject semillaActiva;
    public GameObject plataformaPrefab;//se asociaria el prefab de la plataforma que crearía
    private GameObject plataformaCreada;
    private float offset;
    [SerializeField]
    private string tagPared;
    [SerializeField]
    private string tagSuelo;//Creo las dos tags para diferenciar cuando debe instanciarse en horizontal o en vertical, en funcion de donde impacte la semilla
    #endregion

    #region Contructores

    void Start()
    {
        //Indicamos que no existen plataformas creadas al iniciar 
        creado = false;
        semillaActiva = this.gameObject;
        offset = plataformaPrefab.transform.localScale.y / 2;
        Debug.Log(offset);
    }

    #endregion
    private void FixedUpdate()
    {
        LineCollided(transform.position, 0.2f);
    }

    #region Metodo Privado


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Revisamos que colisiona con Pared
        //if (collision.gameObject.CompareTag(tagSuelo))
        //{
        //    CrearPlataforma(false);
        //}
        //else if (collision.gameObject.CompareTag(tagPared))
        //{
        //    CrearPlataforma(true);
        //}
        //Destroy(semillaActiva);
    }

    /// <summary>
    ///  Metodo para crear la plataforma tras revisar si hay o no creada anteriormente una plataforma. En caso de que no hay plataforma se crearía 
    ///  una plataforma en la ubicacion de la semilla en el momento en el que se llama al metodo además se invoca el método Destruir en 8 segundos.
    ///  En el caso de que ya hay una plataforma, se destruye la actual y pasa a estado false y se vuelve a llamar al metodo CrearPlataforma
    /// </summary>

    private void CrearPlataforma(bool horizontal)
    {
        float direccionY = Mathf.Sign(semillaActiva.GetComponent<Rigidbody2D>().velocity.y);
        float direccionX = Mathf.Sign(semillaActiva.GetComponent<Rigidbody2D>().velocity.x);
        Debug.Log($"{semillaActiva.GetComponent<Rigidbody2D>().velocity.x} velocity x, {semillaActiva.GetComponent<Rigidbody2D>().velocity.y}y");
        Vector2 posicionPlataforma = horizontal ?
            new Vector2(semillaActiva.transform.position.x + offset * -direccionX, semillaActiva.transform.position.y) :
        new Vector2(semillaActiva.transform.position.x, semillaActiva.transform.position.y + offset * direccionY);
        //Vector2 posicionPlataforma = new Vector2(semillaActiva.transform.position.x, semillaActiva.transform.position.y + offset);
        float rotationDeg = horizontal ? 90f : 0f;
        if (!creado)
        {
            plataformaCreada = Instantiate(plataformaPrefab, posicionPlataforma, Quaternion.identity);
            plataformaCreada.transform.Rotate(0, 0, rotationDeg);
            plataformaCreada.GetComponent<Platform>().StartDestructionTimer();
            creado = true;
        }
        else if (creado)
        {
            plataformaCreada.GetComponent<Platform>().DestroyPlatform();
            creado = false;
            CrearPlataforma(horizontal);
        }
    }
    private bool LineCollided(Vector3 position, float stepSize)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, stepSize);
        foreach (Collider2D hit in hits) { 
        if (hit.CompareTag(tagSuelo))
        {
            CrearPlataforma(false); Destroy(semillaActiva);
            }
        else if (hit.CompareTag(tagPared))
        {
            CrearPlataforma(true); Destroy(semillaActiva);
        }
        }
        return hits.Length > 0;
    }

    #endregion
}

