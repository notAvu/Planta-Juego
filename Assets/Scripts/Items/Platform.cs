using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static GameObject activePlatform;
    [SerializeField]
    private float lifeSpan = 2f;
    private void Start()
    {
        if( activePlatform!= null)
        {
            Destroy(activePlatform);
        }
        activePlatform = this.gameObject;
        StartCoroutine(nameof(DestructionTimer));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.otherRigidbody.AddForce(new Vector2(30,0));
        }
    }
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
