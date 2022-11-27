using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour
{
    [SerializeField] 
    private GameObject mainCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainCamera.transform.parent = gameObject.transform;
            mainCamera.transform.localPosition = Vector3.zero;
        }
    }
}
