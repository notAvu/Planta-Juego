using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_zone: MonoBehaviour
{
    private bool DentroArea;
    public GameObject jugador;
    public float velocidadRegeneracion;
    private void Update()
    {
        //comprueba que DentroArea es verdadera y si lo es ejecuta la funcion Regenrarvida
        if (DentroArea)
        {

            Regenrarvida();
        }
    }
    /// <summary>
    /// Comprueba que el jugador  a entrado en  la zona de luz y si esta dentro  se  habilita DentroArea
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DentroArea = true;
        }
    }
    /// <summary>
    /// Comprueba que el jugador se a salido de la zona de luz y su es asi desabilita DentroArea
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            DentroArea = false;
        }
       
    }
    /// <summary>
    /// La funcion Regenrarvida comprueba que la vida del jugador no esta al tope y si no lo esta le agrega vida al jugador
    /// </summary>
    public void Regenrarvida(){

            jugador.gameObject.GetComponent<PlayerController>().AñadirVida(velocidadRegeneracion);
 
    }
}
