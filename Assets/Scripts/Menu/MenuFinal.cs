using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuFinal : MonoBehaviour
{
    public GameObject finPanel;

    // Start is called before the first frame update
    void Start()
    {
        finPanel.SetActive(false);
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
        Time.timeScale = 0;
        finPanel.SetActive(true);
    }

}
