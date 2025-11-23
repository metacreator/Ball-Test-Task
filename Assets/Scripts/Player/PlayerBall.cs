using System;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public event Action<float> OnScaleChanged;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float minAllowedFraction = 0.1f;

    public bool IsMoving => playerMovement.IsMoving;
    public float MinAllowedScale => _originalScale * minAllowedFraction;
    public float CurrentScale => transform.localScale.x;
    public bool IsTooSmall => CurrentScale <= MinAllowedScale;
    public float OriginalScale => _originalScale;


    private float _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale.x;
    }

    public void SetScale(float s)
    {
        transform.localScale = Vector3.one * s;
        OnScaleChanged?.Invoke(s);
    }

    public void StopPlayerMovement()
        => playerMovement.StopImmediatelyInFrontOfDoor(transform.position);
}