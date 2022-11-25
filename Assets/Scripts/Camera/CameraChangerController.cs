using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangerController : MonoBehaviour
{
    public string direccion;
    private Transform trCamara;
    private Camera camara;
    private Vector2 UnidadesEnCamara;

    private void Awake()
    {
        trCamara = GameObject.Find("MainCamera").transform.GetComponentInParent<Transform>();
        camara = GameObject.Find("MainCamera").transform.GetComponentInParent<Camera>();
        //Calculo de las unidades de la c�mara
        UnidadesEnCamara.y = camara.orthographicSize * 2;
        UnidadesEnCamara.x = UnidadesEnCamara.y * Screen.width / Screen.height;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CambiaPosicionCamara();
        }
    }

    /// <summary>
    /// M�todo que cambia la posici�n de la c�mara en funci�n del tama�o de la c�mara, de la posici�n en la que est� y 
    /// el lado por el que est� saliendo el jugador
    /// </summary>
    private void CambiaPosicionCamara()
    {
        if (direccion.Equals("RIGHT"))
        {
            trCamara.SetPositionAndRotation(new Vector3(trCamara.position.x + UnidadesEnCamara.x, trCamara.position.y, trCamara.position.z), trCamara.rotation);
        }
        else if(direccion.Equals("LEFT"))
        {
            trCamara.SetPositionAndRotation(new Vector3(trCamara.position.x - UnidadesEnCamara.x, trCamara.position.y, trCamara.position.z), trCamara.rotation);
        }
        else if (direccion.Equals("TOP"))
        {
            trCamara.SetPositionAndRotation(new Vector3(trCamara.position.x, trCamara.position.y + UnidadesEnCamara.y, trCamara.position.z), trCamara.rotation);
        }
        else if (direccion.Equals("BOTTOM"))
        {
            trCamara.SetPositionAndRotation(new Vector3(trCamara.position.x, trCamara.position.y - UnidadesEnCamara.y, trCamara.position.z), trCamara.rotation);
        }
    }
}
