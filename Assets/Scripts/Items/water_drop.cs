using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_drop: MonoBehaviour
{
    /// <summary>
    /// Al entrar la gota en colision con el jugador se le agrega al jugador vida y la gota se destruye 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) {

            collision.gameObject.GetComponent<PlayerController>().AñadirVida(20f);
            Destroy(gameObject);
        }
    }


}
