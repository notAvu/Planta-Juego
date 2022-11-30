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
    [SerializeField] private Sprite defaultImage, usedImage;
    private float time;
    public GunPoint playerController;
    #endregion

    #region Unity methods
    void Start()
    {
        //Cambiar la barra de vida al comenzar
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

    /// <summary>
    ///     <b>Cabecera: </b>public void SetTimeBar(float normalizedValue)
    ///     <b>Descripción: </b> Cambia el tamnaño de la barra de vida con respecto al valor dado
    /// </summary>
    /// <param name="normalizedValue"> float con el tiempo normalizado</param>
    private void SetTimeBar(float normalizedValue)
    {
        timeBar.transform.localScale = new Vector3(normalizedValue, 1.0f);
    }


    /// <summary>
    ///     <b>Cabecera: </b>private void SetPlayerSeeds()
    ///     <b>Descripción: </b> Cambia la interfaz de las semillas restantes con respecto al numero de semillas
    /// </summary>
    public void SetPlayerSeeds()
    {
        switch (playerController.availableSeeds)
        {
            case 0:
                if (textContadorSeed1.gameObject.activeInHierarchy && !textContadorSeed2.gameObject.activeInHierarchy)
                {
                    ChangeSeedImage(seed2, usedImage);
                    HUD_CoolDown auxCooldown = new HUD_CoolDown();
                    StartCoroutine(auxCooldown.StartCountdown(textContadorSeed2));
                }
                else if (!textContadorSeed1.gameObject.activeInHierarchy && textContadorSeed2.gameObject.activeInHierarchy)
                {
                    ChangeSeedImage(seed1, usedImage);
                    HUD_CoolDown auxCooldown = new HUD_CoolDown();
                    StartCoroutine(auxCooldown.StartCountdown(textContadorSeed1));
                }
                break;

            case 1:
                if(!textContadorSeed1.gameObject.activeInHierarchy && !textContadorSeed2.gameObject.activeInHierarchy)
                {
                    ChangeSeedImage(seed1, defaultImage);
                    ChangeSeedImage(seed2, usedImage);
                    HUD_CoolDown auxCooldown = new HUD_CoolDown();
                    StartCoroutine(auxCooldown.StartCountdown(textContadorSeed2));
                }
                else if (textContadorSeed1.gameObject.activeInHierarchy && !textContadorSeed2.gameObject.activeInHierarchy)
                {
                    ChangeSeedImage(seed2, defaultImage);
                }
                else
                {
                    ChangeSeedImage(seed1, defaultImage);
                }
                break;

            case 2:
                ChangeSeedImage(seed1, defaultImage);
                ChangeSeedImage(seed2, defaultImage);
                break;
        }
    }

    /// <summary>
    ///     <b>Cabecera: </b>private void ChangeSeedImage(GameObject seed, Sprite sprite)
    ///     <b>Descripción: </b> Cambia el sprite de la imagen de las semillas
    /// </summary>
    private void ChangeSeedImage(GameObject seed, Sprite sprite)
    {
        seed.GetComponent<Image>().sprite = sprite;
    }
    #endregion

    public float getTime()
    {
        return time;
    }

}
