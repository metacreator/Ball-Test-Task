using System;
using UnityEngine;

public class ChargeScaler
{
    public event Action OnPlayerTooSmall;

    private Projectile _projectile;
    private readonly PlayerBall _player;
    private readonly float _minProjectileScale;
    private readonly float _chargeSpeed;

    private float _currentScale;
    private bool _charging;

    public ChargeScaler(PlayerBall player, float minProjectileScale, float chargeSpeed)
    {
        _player = player;
        _minProjectileScale = minProjectileScale;
        _chargeSpeed = chargeSpeed;
    }

    public void InitializeProjectile(Projectile projectile)
    {
        _projectile = projectile;
        _currentScale = _minProjectileScale;
    }

    public void StartCharging()
    {
        if (_player.IsTooSmall)
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

    public void Tick(float deltaTime)
    {
        if (!_charging || !_projectile) return;

        var grow = _chargeSpeed * deltaTime;

        var nextProjectile = _currentScale + grow;
        var nextPlayer = _player.CurrentScale - grow;

        if (nextPlayer <= _player.MinAllowedScale)
        {
            _charging = false;
            OnPlayerTooSmall?.Invoke();
            return;
        }

        float absoluteMaxProjectile = _player.OriginalScale - 0.1f; 

        if (nextProjectile > absoluteMaxProjectile)
        {
            _charging = false;
            return;
        }


        _currentScale = nextProjectile;
        _projectile.SetScale(_currentScale);

        _player.SetScale(nextPlayer);
    }
}