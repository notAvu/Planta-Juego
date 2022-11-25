using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float lifeSpan = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DestructionTimer()
    {
        yield return new WaitForSeconds(lifeSpan);
        DestroyPlatform();
    }
    public void StartDestructionTimer()
    {
        StartCoroutine("DestructionTimer");
    }
    public void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
