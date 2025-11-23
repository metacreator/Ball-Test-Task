using System;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public event Action<float> OnScaleChanged;

    [SerializeField] private float minAllowedScale = 0.2f;
    [SerializeField] private PlayerMovement playerMovement;

    public bool IsMoving => playerMovement.IsMoving;
    public float MinAllowedScale => minAllowedScale;
    public float CurrentScale => transform.localScale.x;
    public bool IsTooSmall => transform.localScale.x <= minAllowedScale;

    public void SetScale(float s)
    {
        transform.localScale = Vector3.one * s;
        OnScaleChanged?.Invoke(s);
    }

    public void StopPlayerMovement()
        => playerMovement.StopImmediatelyInFrontOfDoor(transform.position);
}