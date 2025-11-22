using UnityEngine;
using DG.Tweening;

public class ChargeScaler
{
    private readonly float _minScale;
    private readonly float _maxScale;
    private readonly float _pulseDuration;

    private Tween _pulseTween;
    private float _lastScale;

    private readonly Projectile _projectile;
    private readonly PlayerBall _playerBall;

    public ChargeScaler(Projectile projectile, PlayerBall playerBall,
        float minScale, float maxScale, float pulseDuration)
    {
        _projectile = projectile;
        _playerBall = playerBall;

        _minScale = minScale;
        _maxScale = maxScale;
        _pulseDuration = pulseDuration;

        _lastScale = minScale;
        projectile.SetScale(minScale);
    }

    public void StartPulse()
    {
        _pulseTween = DOTween.Sequence()
            .AppendCallback(() => PulseTo(_maxScale))
            .AppendInterval(_pulseDuration)
            .AppendCallback(() => PulseTo(_minScale))
            .AppendInterval(_pulseDuration)
            .SetLoops(-1);
    }

    private void PulseTo(float target)
    {
        DOTween.To(
            () => _lastScale,
            value =>
            {
                var delta = value - _lastScale;
                _lastScale = value;

                _projectile.SetScale(value);
                _playerBall.ChangeScale(-delta);
            },
            target,
            _pulseDuration
        ).SetEase(Ease.InOutSine);
    }

    public void StopPulse()
    {
        _pulseTween?.Kill();
        DOTween.Kill(_projectile.transform);
    }
}