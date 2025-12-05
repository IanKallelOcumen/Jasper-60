using UnityEngine;
using UnityEngine.Events;

public class BalanceCheck : MonoBehaviour
{
    [Header("Balance Settings")]
    [SerializeField][Range(0f, 45f)] float maxTilt = 25f;
    [SerializeField] UnityEvent OnFall;

    void Update()
    {
        float tilt = Mathf.Abs(transform.rotation.eulerAngles.z);
        if (tilt > maxTilt && tilt < 360f - maxTilt)
        {
            OnFall.Invoke();
            enabled = false; // stop repeating
        }
    }
}