using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region Atributos

    private float velocidad, fuerzaSalto;
    private Rigidbody2D rigid;
    private Collider2D capCollider;
    private SpriteRenderer spriteRenderer;
    public LayerMask capaSuelo;
    private Animator animador;
    public float VidaTotal;
    public float VidaActual;
    public string tagHiedra;
    public string tagCuervo;
    public string tagSalida;
    private MenuFinal menuFinal;
    #endregion

    #region Contructores

    private void Start()
    {
        menuFinal = GameObject.Find("GameController").GetComponent<MenuFinal>();
        velocidad = 10f;
        fuerzaSalto = 6.8f;
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    #endregion

    #region Metodos publicos

    public void AñadirVida(float vida)
    {

        if (VidaActual < VidaTotal)
        {
            VidaActual += vida;
        }
    }

    #endregion

    #region Metodos privados

    private void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();

        //AnimarJugador();
    }

    /// <summary>
    /// Procesa el salto, si el jugador ha pulsado la tecla Espacio se aplicar� la fuerzaSalto con la direccion Vector2.up
    /// </summary>
    private void ProcesarSalto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo())
        {
            rigid.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Procesa el movimiento, si el jugador no esta en el suelo la variable restaVelocidad ser� mayor que 0, por lo que la velocidad horizontal disminuye
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
        else if (movHorizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Se crea un raycast, si los pies del jugador est�n tocando el suelo devolver� true, de lo contrario devolver� false
    /// </summary>
    /// <returns>Bool</returns>
    private bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capCollider.bounds.center, new Vector2(capCollider.bounds.size.x / 2, capCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Se dan las animaciones al personaje
    /// </summary>
    /*
    private void AnimarJugador()
    {
        //Si est� saltando
        if (!EstaEnSuelo())
        {
            animador.SetBool("saltando", true);
            animador.SetBool("corriendo", false);
        }
        //Si est� en movimiento
        else if (Mathf.Abs(rigid.velocity.x) > 0.1 && Mathf.Abs(rigid.velocity.y) == 0)
        {
            animador.SetBool("corriendo", true);
            animador.SetBool("saltando", false);
        }
        //Si est� quieto
        else
        {
            animador.SetBool("corriendo", false);
            animador.SetBool("saltando", false);
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagCuervo))
        {
            //tras colisionar con el cuervo se reinicia el nivel.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else if (collision.gameObject.CompareTag(tagHiedra))
        {
            DañoHiedra();
        }
        else if (!collision.gameObject.CompareTag(tagHiedra))
        {
            //Si no toca la hiedra vuelve a velocidad inicial
            velocidad = 10f;
        }
        if (collision.gameObject.CompareTag(tagSalida))
        {
           
            menuFinal.Salida();
        }


    }

    private void DañoHiedra()
    {

        //se revisa si está tocando la hiedra para reducir la velocidad e ir disminuyendo la vida actual (Pendiente de valores)
        velocidad = 8f;
        VidaActual = VidaActual - 1f;

    }

    #endregion
}
