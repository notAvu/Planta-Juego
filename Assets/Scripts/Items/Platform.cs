using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 2f;
    private void Start()
    {
        StartCoroutine(nameof(DestructionTimer));

    }
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
