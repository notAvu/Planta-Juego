using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Controller : MonoBehaviour
{
    #region Attributes
    [SerializeField] private GameObject timeBar, seed1, seed2;
    [SerializeField] private TextMeshProUGUI textTime, textContadorSeed1, textContadorSeed2;
    [SerializeField] private float maxTime = 60;
    [SerializeField] private Material materialOpaco, materialDefault;
    private float time;
    private PlayerController playerController;
    private static int auxcount = 8;

    #endregion

    #region Unity methods
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        SetTimeBar((maxTime - time) / maxTime);
    }

    void Update()
    {
        //actualizar texto tiempo como contador
        textTime.text = SetTime();
        //actualizar barra tiempo
        StartCoroutine(SetSmoothTimeBar());
        //actualizar semillas
        SetPlayerSeeds();
        if (auxcount <= 0)
        {
            CancelInvoke(nameof(SetCountdownSeed1));
            auxcount = 8;
            playerController.Semillas = 2;
            textContadorSeed1.enabled = false;
        }
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

    /// <summary>
    ///     <b>Cabecera: </b>private IEnumerator SetSmoothTimeBar()
    ///     <b>Descripción: </b> Baja la barra de tiempo acorde con un tiempo máximo predeterminado
    /// </summary>
    /// <returns></returns>
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

    public void SetTimeBar(float normalizedValue)
    {
        timeBar.transform.localScale = new Vector3(normalizedValue, 1.0f);
    }


    /// <summary>
    ///     <b>Cabecera: </b>private void SetPlayerSeeds()
    ///     <b>Descripción: </b> Cambia la interfaz de las semillas restantes con respecto al numero de semillas
    /// </summary>
    private void SetPlayerSeeds()
    {
        switch (playerController.Semillas)
        {
            case 0:
                seed1.GetComponent<Image>().material = materialOpaco;
                seed2.GetComponent<Image>().material = materialOpaco;
                break;

            case 1:
                seed1.GetComponent<Image>().material = materialDefault;
                seed2.GetComponent<Image>().material = materialOpaco;
                InvokeRepeating(nameof(SetCountdownSeed1), 0,1000000.0f);
                break;

            case 2:
                seed1.GetComponent<Image>().material = materialDefault;
                seed2.GetComponent<Image>().material = materialDefault;
                break;
        }
    }

    public void SetCountdownSeed1()
    {
        textContadorSeed1.enabled = true;
        Debug.Log(auxcount);
        textContadorSeed1.text = $"{auxcount}";
        auxcount--;
    }

    #endregion
}

