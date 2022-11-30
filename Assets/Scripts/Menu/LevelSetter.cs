using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSetter : MonoBehaviour
{
    #region variables
    /// <summary>
    /// objeto que contiene el controlador del menu
    /// </summary>
    [SerializeField] private MenuController menu;
    /// <summary>
    /// nombre de la escena a la que llama el bot�n
    /// </summary>
    public string levelSceneName;
    /// <summary>
    /// referencia al objeto rankingSaver
    /// </summary>
    public RankingSaver rankingSaver;
    /// <summary>
    /// Texto en el que mostrar el ranking
    /// </summary>
    [SerializeField] private TextMeshProUGUI rankingText;
    #endregion

    /// <summary>
    /// Buscamos el objeto rankigSaver
    /// </summary>
    void Start()
    {
        rankingSaver = GameObject.Find("Ranking_Saver").GetComponent<RankingSaver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Cuando se le haga click llamar� al metodo de seleccion de nivel del menu
    /// Mostrar� el ranking en el campo de texto
    /// </summary>
    public void OnBotonPulsado()
    {
        menu.OnButtonLevel(levelSceneName);
        ShowRanking(levelSceneName);
    }

    /// <summary>
    /// Muestra los 15 primeros registros del ranking de la escena seleccionada
    /// </summary>
    /// <param name="levelName">nombre de la escena seleccionada</param>
    public void ShowRanking(string levelName)
    {
        rankingText.SetText(rankingSaver.ShowLevelRank(levelName,15));
    }


}
