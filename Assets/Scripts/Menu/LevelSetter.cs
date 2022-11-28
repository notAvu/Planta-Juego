using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSetter : MonoBehaviour
{
    #region variables
    [SerializeField] private MenuController menu;
    public string levelSceneName;
    public RankingSaver rankingSaver;
    [SerializeField] private TextMeshProUGUI rankingText;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rankingSaver = GameObject.Find("Ranking_Saver").GetComponent<RankingSaver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Cuando se le haga click llamará al metodo de seleccion de nivel del menu
    /// </summary>
    public void OnBotonPulsado()
    {
        menu.OnButtonLevel(levelSceneName);
        ShowRanking(levelSceneName);
    }

    public void ShowRanking(string levelName)
    {
        rankingText.SetText(rankingSaver.ShowLevelRank(levelName,15));
    }


}
