using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Atributos control Pausa

    #endregion
    public GameObject FloatingPlatform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarNivel();
        }   
    }
    /// <summary>
    /// Metodo para reiniciar la escena actual.
    /// </summary>
    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReponerPlataforma(Vector3 localizacion) {

        Instantiate(FloatingPlatform, localizacion, Quaternion.identity);

    }
}
