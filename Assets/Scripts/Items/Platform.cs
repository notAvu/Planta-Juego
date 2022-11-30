using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static GameObject activePlatform;
    [SerializeField]
    private float lifeSpan = 8f;
    [SerializeField]
    GameObject origin;
    //int scalingFramesLeft;

    private void Start()
    {

        if (activePlatform != null)
        {
            Destroy(activePlatform);
        }
        activePlatform = gameObject;
        //scalingFramesLeft = 60;
        StartCoroutine(nameof(DestructionTimer));
        StartCoroutine(Grow());
    }
    private IEnumerator Grow()
    {
        Vector3 offset = transform.position - origin.transform.position;
        Vector2 originalScale = new Vector2(0.01f, transform.localScale.y);
        Vector2 destinationScale = new Vector2(1.0f , transform.localScale.y);

        float currentTime = 0.0f;
        transform.position -= offset;

        do
        {
            transform.localScale = Vector2.Lerp(originalScale, destinationScale, currentTime / 2);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= 2);
    }
    private void Update()
    {
        //if (transform.localScale.x < 1)
        //{
        //    transform.localScale = Vector2.Lerp(new Vector2(0.01f, transform.localScale.y), new Vector2(transform.localScale.x, 1), Time.deltaTime*0.01f);
        //    scalingFramesLeft--;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Vines"))
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Empieza un contador que tras <see cref="lifeSpan"/> segundos destruye la plataforma
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
