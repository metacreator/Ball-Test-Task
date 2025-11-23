using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private float minAllowedScale = 0.2f;
    public float MinAllowedScale => minAllowedScale;
    public float CurrentScale => transform.localScale.x;
    public bool IsTooSmall => transform.localScale.x <= minAllowedScale;

    public void SetScale(float s)
    {
        transform.localScale = Vector3.one * s;
    }
}