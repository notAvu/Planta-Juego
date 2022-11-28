using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaController : MonoBehaviour
{
    #region variables
    [SerializeField] private GameObject pausaPanel;
    [SerializeField] private GameObject rankingTab;
    [SerializeField] private TextMeshProUGUI rankingText;
    public string menuSceneName;
    private bool gamePaused;
    private GameController gameController;
    public RankingSaver rankingSaver;
    private bool rankingShow;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gamePaused = false;
        pausaPanel.SetActive(false);
        rankingTab.SetActive(false);
        rankingShow = false;
        rankingSaver = GameObject.Find("Ranking_Saver").GetComponent<RankingSaver>();
    }

    
    void Update()
    {
        //si pulsas escape
        if (Input.GetButtonDown("Cancel"))
        {
            //si el juego esta pausado
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!gamePaused && !rankingShow)
            {
                ShowRanking();
                rankingShow = true;
            }
            else
            {
                rankingTab.SetActive(false);
                rankingShow = false;
            }
        }
    }
    
    /// <summary>
    /// Pausa el juego. Pone el tiempo a 0 y muestra el menu de pausa
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausaPanel.SetActive(true);
        gamePaused = true;
    }

    /// <summary>
    /// Reanuda el juego
    /// pone el tiempo a 1 y oculta el menu de pausa
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1;
        pausaPanel.SetActive(false);
        gamePaused = false;
    }

    /// <summary>
    /// Caundo se pulse el menu de continuar se llama al método de reanudar
    /// </summary>
    public void OnButtonResume()
    {
        ResumeGame();
    }

    /// <summary>
    /// Llama al método de reinicio
    /// </summary>
    public void OnButtonReset()
    {
        ResumeGame();
        gameController.ReiniciarNivel();
    }

    /// <summary>
    /// Al pulsar el botón de salir se llama al menú del juego (Escena inicial)
    /// </summary>
    public void OnButtonMenu()
    {
        SceneManager.LoadScene(menuSceneName);
        ResumeGame();
    }

    public void ShowRanking()
    {
        rankingText.SetText(rankingSaver.ShowLevelRank(SceneManager.GetActiveScene().name, 15));
        rankingTab.SetActive(true);
        
    }

}
