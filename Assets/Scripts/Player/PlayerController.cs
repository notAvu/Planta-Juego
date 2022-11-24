using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region Atributos

    private float velocidad, fuerzaSalto;
    private Rigidbody2D rigid;
    private CapsuleCollider2D capCollider;
    private SpriteRenderer spriteRenderer;
    public LayerMask capaSuelo;
    private Animator animador;

    #endregion

    #region Contructores

    private void Start()
    {
        velocidad = 10f;
        fuerzaSalto = 6.8f;
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    #endregion

    #region Métodos privados

    private void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        Reinicio();

        //AnimarJugador();
    }

    /// <summary>
    /// Procesa el salto, si el jugador ha pulsado la tecla Espacio se aplicará la fuerzaSalto con la direccion Vector2.up
    /// </summary>
    private void ProcesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo())
        {
            rigid.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Procesa el movimiento, si el jugador no esta en el suelo la variable restaVelocidad será mayor que 0, por lo que la velocidad horizontal disminuye
    /// </summary>
    private void ProcesarMovimiento()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float restaVelocidad = 0;
        if (!EstaEnSuelo())
        {
            restaVelocidad = velocidad * 0.32f;
        }
        rigid.velocity = new Vector2(movHorizontal * (velocidad - restaVelocidad), rigid.velocity.y);
        ProcesaFlip(movHorizontal);
    }

    /// <summary>
    /// Procesa el flip, si el movimiento es mayor que 0 el sentido es derecha, si es menor, es izquierda
    /// </summary>
    /// <param name="movHorizontal"></param>
    private void ProcesaFlip(float movHorizontal)
    {
        if (movHorizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(movHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Se crea un raycast, si los pies del jugador están tocando el suelo devolverá true, de lo contrario devolverá false
    /// </summary>
    /// <returns>Bool</returns>
    private bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, new Vector2(capCollider.bounds.size.x/2, capCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Se dan las animaciones al personaje
    /// </summary>
    /*
    private void AnimarJugador()
    {
        //Si está saltando
        if (!EstaEnSuelo())
        {
            animador.SetBool("saltando", true);
            animador.SetBool("corriendo", false);
        }
        //Si está en movimiento
        else if (Mathf.Abs(rigid.velocity.x) > 0.1 && Mathf.Abs(rigid.velocity.y) == 0)
        {
            animador.SetBool("corriendo", true);
            animador.SetBool("saltando", false);
        }
        //Si está quieto
        else
        {
            animador.SetBool("corriendo", false);
            animador.SetBool("saltando", false);
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Enemy"))
        {

        }*/
    }
    /// <summary>
    /// Metodo llamado desde update para reiniciar la escena actual.
    /// </summary>
    private void Reinicio()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    #endregion
}
