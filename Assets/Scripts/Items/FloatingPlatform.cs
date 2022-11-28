using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform: MonoBehaviour
{
    private Vector3 PosicionOriginal;
    private Rigidbody2D rb;
    private BoxCollider2D BoxC2D;
    public float DuracionPlataforma;
    private bool SobrePlataforma;

    // Start is called before the first frame update
    void Start()
    {
        //Obtengo del objeto su Rigidbody2D , BoxCollider2D y  guardo la posicion de intanciado 
        PosicionOriginal = gameObject.GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        BoxC2D=GetComponent<BoxCollider2D>();
        SobrePlataforma = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {///Cuando SobrePlataforma es verdadero va restando tiempo  a la duracion de la plataforma
        if (SobrePlataforma)
        {
            DuracionPlataforma -= Time.deltaTime;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /// Al colisionar con el jugador activa un bool y activa un temporizador cuando llega a cero  
        /// cambia el  tipo de cuerpo de la plataforma  a Dynamic aumento su gravedad 
        /// y le digo que es un trigger para que atraviese el suelo y lo destruyo a los 5s
        if (collision.gameObject.CompareTag("Player"))
        {
            SobrePlataforma = true;
            if (DuracionPlataforma <= 0)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 5;
            BoxC2D.isTrigger = true;
            Destroy(gameObject, 5);
                 }
        }
    }


    /// <summary>
    /// Cuando es destruido llama al GameController para volver a invocarlo en el mismo lugar pasandole la posicion Original
    /// </summary>
    private void OnDestroy()
    {
        gameObject.GetComponent<GameController>().ReponerPlataforma(PosicionOriginal);
    }

 

}
