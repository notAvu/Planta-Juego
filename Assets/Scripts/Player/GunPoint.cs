using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform gunHolder;
    [SerializeField]
    private Transform gunPivot;
    private Transform firePoint;

    private void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RotateGun(mousePosition);
    }
    private void RotateGun(Vector3 targetPoint)
    {
        Vector3 distanceVector = targetPoint - gunPivot.position;
        float angleDeg = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.AngleAxis(angleDeg, Vector3.forward);
    }
}
