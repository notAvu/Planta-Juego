using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Controller : MonoBehaviour
{
    #region Attributes
    [SerializeField] private GameObject timeBar, seed1, seed2;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private float maxTime = 60;
    private float time;
    private PlayerController playerController;

    #endregion

    #region Unity methods
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //actualizar texto tiempo como contador
        textTime.text = SetTime();
        //actualizar barra tiempo
        StartCoroutine(SetSmoothTimeBar());
        //actualizar semillas
        SetPlayerSeeds();
    }
    #endregion

    #region Methods
    /// <summary>
    ///     <b>Cabecera: </b>private string SetTime
    ///     <b>Descripción: </b> Cambia el texto con el tiempo transcurrido en la partida
    /// </summary>
    /// <returns> Cadena con el tiempo en minutos y segundos</returns>
    private string SetTime()
    {

        //Añado el intervalo transcurrido a la variable
        time += Time.deltaTime;
        

        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(time / 60).ToString("00");
        string segundos = Mathf.Floor(time % 60).ToString("00");

        //Devuelvo el string formateado con : como separador
        return minutos + ":" + segundos;
    }

    private IEnumerator SetSmoothTimeBar()
    {
        float normalizedValue = (maxTime - time) / maxTime;
        float currentScale = timeBar.transform.localScale.x;
        float updateQuantity = currentScale - normalizedValue;
        while (currentScale - normalizedValue > Mathf.Epsilon && currentScale > 0)
        {
            currentScale -= updateQuantity * Time.deltaTime;
            timeBar.transform.localScale = new Vector3(currentScale, 1);
            yield return null;
        }

        if (currentScale <= 0)
        {
            timeBar.transform.localScale = new Vector3(0, 1);
        }
        else
        {
            timeBar.transform.localScale = new Vector3(normalizedValue, 1);
        }
    }

    /*
     * private void SetPlayerSeeds()
    {
        switch (playerController.Seeds)
        {
            case 0:
                seed1.GetComponent<Image>().color = Color.grey;
                seed2.GetComponent<Image>().color = Color.grey;
                break;

            case 1:
                seed1.GetComponent<Image>().color = new Color(219,36,180,255);
                seed2.GetComponent<Image>().color = Color.grey;
                break;

            case 2:
                seed1.GetComponent<Image>().color = new Color(219, 36, 180, 255);
                seed2.GetComponent<Image>().color = new Color(219, 36, 180, 255);
                break;
        }
    }
     * */

    #endregion
}
