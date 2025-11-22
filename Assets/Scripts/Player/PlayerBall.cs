using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public void ChangeScale(float delta)
    {
        var newScale = Mathf.Max(0.1f, transform.localScale.x + delta);
        transform.localScale = Vector3.one * newScale;
    }
}