using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool _infected;

    public void Infect()
    {
        if (_infected) return;
        _infected = true;

        // TODO: VFX, shake, particles, animation
        Destroy(gameObject);
    }
}