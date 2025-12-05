using UnityEngine;

public class HouseFollower : MonoBehaviour
{
    [SerializeField] Transform carLeft;
    [SerializeField] Transform carRight;
    [SerializeField] float heightOffset = 1.2f; // tune until house sits on pole

    Vector3 startPos; // initial local offset from pole center
    Quaternion startRot;

    void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void LateUpdate()
    {
        // 1. follow midpoint of the two cars
        Vector3 mid = (carLeft.position + carRight.position) * 0.5f;
        transform.position = new Vector3(mid.x, mid.y + heightOffset, mid.z);

        // 2. stay perfectly upright
        transform.rotation = startRot;
    }
}