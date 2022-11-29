using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_drop: MonoBehaviour
{
    public float curacionGota;
    /// <summary>
    /// Al entrar la gota en colision con el jugador se le agrega al jugador vida y la gota se destruye 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerController>().AñadirVida(curacionGota);
            Destroy(gameObject);
        }
    }

}
