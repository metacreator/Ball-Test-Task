using UnityEngine;

public class ChargeScaler
{
    private readonly Projectile _projectile;
    private readonly PlayerBall _playerBall;

    private readonly float _minScale;
    private readonly float _maxScale;
    private readonly float _chargeSpeed;

    private float _currentScale;
    private bool _charging;

    public ChargeScaler(
        Projectile projectile,
        PlayerBall playerBall,
        float minScale,
        float maxScale,
        float chargeSpeed)
    {
        _projectile = projectile;
        _playerBall = playerBall;

        _minScale = minScale;
        _maxScale = maxScale;
        _chargeSpeed = chargeSpeed;

        _currentScale = minScale;

        _projectile.SetScale(_currentScale);
    }

    public void StartCharging()
    {
        _charging = true;
    }

    public void StopCharging()
    {
        _charging = false;
    }

    public void Tick(float dt)
    {
        if (!_charging) return;

        if (_currentScale >= _maxScale)
            return;

        float newScale = _currentScale + _chargeSpeed * dt;
        float delta = newScale - _currentScale;

        _currentScale = newScale;

        _projectile.SetScale(_currentScale);
        _playerBall.ChangeScale(-delta);
    }
}