using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    #region Atributos
    public float velocidad;
    public float limiteMov;
    public bool movHorizontal;

    private Vector3 posicionDestino;
    private Vector3 posicionInicial;

    private SpriteRenderer spriteRenderer;

    #endregion

    #region Contructores
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ProcesaPosicionamiento();
        ProcesaFlip();
    }
    #endregion

    #region M�todos privados
    private void Update()
    {
        Mueve();
    }

    /// <summary>
    /// Procesa la direcci�n y sentido inicial.
    /// El sentido inicial es aleatorio si el movimiento es horizontal, en caso contrario 
    /// la direcci�n va en funci�n de el atributo p�blico booleano movHorizontal.
    /// Si movHorizontal es true el movimiento ser� horizontal en caso contrario el movimiento ser� vertical
    /// </summary>
    private void ProcesaPosicionamiento()
    {
        float movXIni = transform.position.x;
        float movYIni = transform.position.y;
        float movXDest = transform.position.x;
        float movYDest = transform.position.y;
        int sentido = 1;

        //Seteo las pociciones iniciales y de destino seg�n si es movimiento horizontal o vertical
        if (movHorizontal)
        {
            movXIni -= limiteMov;
            movXDest += limiteMov;

            sentido = Random.Range(0, 2);
        }
        else
        {
            movYIni -= limiteMov;
            movYDest += limiteMov;
        }

        //Seg�n el sentido la primera posici�n a la que se mover� ser� el movIni o el movDest
        if (sentido == 0)
        {
            //Izquierda
            posicionDestino = new Vector3(movXIni, movYIni, transform.position.z);
            posicionInicial = new Vector3(movXDest, movYDest, transform.position.z);
        }
        else
        {
            //Derecha
            posicionDestino = new Vector3(movXDest, movYDest, transform.position.z);
            posicionInicial = new Vector3(movXIni, movYIni, transform.position.z);
        }
    }

    /// <summary>
    /// Mueve de un punto a otro al enemigo e intercambia las posiciones iniciales y de destino para que est� vigilando un �rea
    /// </summary>
    private void Mueve()
    {
        if (transform.position == posicionDestino)
        {
            posicionDestino = posicionInicial;
            posicionInicial = transform.position;
            ProcesaFlip();
        }
        transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
    }

    /// <summary>
    /// Procesa el flip, si la posicion inicial es menor que la posici�n destino el sentido es derecha, si es al contrario, es izquierda
    /// </summary>
    private void ProcesaFlip()
    {
        if (posicionInicial.x < posicionDestino.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    #endregion
}
