using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuFinal : MonoBehaviour
{
    public GameObject finPanel;
    public GameObject textPuntos;
    public GameObject textColeccionables;



    private string puntos;
    private string coleccionables;


    // Start is called before the first frame update
    void Start()
    {
        finPanel.SetActive(false);
        puntos = "10"; //Prueba para verificar que indica los puntos totales
        coleccionables = "2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Se llama desde Player Controller cuando el personaje llega a la salida.
    /// </summary>
    public void Salida()
    {
        Time.timeScale = 0; //Al finalizar nivel se pausa el juego (Se debe volver a activar el juego tras cambiar de nivel)
        finPanel.SetActive(true);
        textPuntos.GetComponent<TextMeshProUGUI>().text = puntos;
        textColeccionables.GetComponent<TextMeshProUGUI>().text = coleccionables;

        
    }

}
