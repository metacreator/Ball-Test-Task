using System;
using UnityEngine;

public class ChargeScaler
{
    public event Action OnPlayerTooSmall;

    private Projectile _projectile;
    private readonly PlayerBall _player;
    private readonly float _minScale;
    private readonly float _maxScale;
    private readonly float _chargeSpeed;

    private float _currentScale;
    private bool _charging;

    public ChargeScaler(PlayerBall player, float minScale, float maxScale, float chargeSpeed)
    {
        _player = player;
        _minScale = minScale;
        _maxScale = maxScale;
        _chargeSpeed = chargeSpeed;
    }

    public void InitializeProjectile(Projectile projectile)
    {
        _projectile = projectile;
        _currentScale = _minScale;
    }

    public void StartCharging()
    {
        if (_player.CurrentScale <= _player.MinAllowedScale)
        {
            OnPlayerTooSmall?.Invoke();
            return;
        }

        _charging = true;
        _projectile.SetScale(_currentScale);
    }

    public void StopCharging()
    {
        _charging = false;
    }

    public void Tick(float dt)
    {
        if (!_charging || !_projectile) return;

        var grow = _chargeSpeed * dt;

        var nextProjectile = _currentScale + grow;
        var nextPlayer = _player.CurrentScale - grow;

        if (nextPlayer <= _player.MinAllowedScale)
        {
            _charging = false;
            OnPlayerTooSmall?.Invoke();
            return;
        }

        if (nextProjectile > _maxScale)
        {
            _charging = false;
            return;
        }

        _currentScale = nextProjectile;
        _projectile.SetScale(_currentScale);

        _player.SetScale(nextPlayer);
    }
}