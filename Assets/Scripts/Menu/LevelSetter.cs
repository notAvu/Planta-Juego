using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetter : MonoBehaviour
{
    #region variables
    [SerializeField] private MenuController menu;
    public string levelSceneName;



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
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
    }


}
