using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_zone: MonoBehaviour
{
    public GameObject jugador;
    public float velocidadRegeneracion;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jugador.gameObject.GetComponent<PlayerController>().AñadirVida(velocidadRegeneracion);
        }
    }
}
