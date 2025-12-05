using UnityEngine;

public class PoleBalancer : MonoBehaviour
{
    [SerializeField] Transform carLeft;
    [SerializeField] Transform carRight;
    [SerializeField] float maxAngle = 20f;

    void Update()
    {
        float heightDiff = carLeft.position.y - carRight.position.y;
        float targetAngle = Mathf.Clamp(heightDiff * 30f, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, -targetAngle);
    }
}