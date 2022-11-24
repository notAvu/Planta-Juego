using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    //Se asociaria al prefab de la Semilla que creará la plataforma
    #region Atributos

    //booleano para comprobar si hay o no plataforma creada
    private bool creado;
    private GameObject semilla;
    public GameObject plataforma;//se asociaria el prefab de la plataforma que crearía

    #endregion

    #region Contructores

    void Start()
    {
        //Indicamos que no existen plataformas creadas al iniciar 
        creado = false;
        semilla = GameObject.FindWithTag("Semilla");
    }

    #endregion

    #region Metodo Privado


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Revisamos que colisiona con Pared
        if (collision.gameObject.CompareTag("Pared"))
        {
            CrearPlataforma();
        }
    }

    /// <summary>
    ///  Metodo para crear la plataforma tras revisar si hay o no creada anteriormente una plataforma. En caso de que no hay plataforma se crearía 
    ///  una plataforma en la ubicacion de la semilla en el momento en el que se llama al metodo además se invoca el método Destruir en 8 segundos.
    /// En el caso de que ya hay una plataforma, se destruye la actual y pasa a estado false y se vuelve a llamar al metodo CrearPlataforma
    /// </summary>

    private void CrearPlataforma()
    {
        if (creado == false)
        {
            Instantiate(plataforma, semilla.transform.position, Quaternion.identity);
            Invoke("Destruir", 8f);
            creado = true;
        } 
        else if (creado == true)
        {
            Destroy(GameObject.FindWithTag("plataforma"));
            creado = false;
            CrearPlataforma();
        }
    }

    //Metodo para destruir que se invoca tras 8 segundos tras haber creado la plataforma
    private void Destruir()
    {
        Destroy(GameObject.FindWithTag("plataforma"));
        creado = false;
    }



    #endregion
}

