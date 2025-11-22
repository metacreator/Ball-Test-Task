using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerBall playerBall;
    
    [SerializeField] private float minScale = 0.15f;
    [SerializeField] private float maxScale = 0.35f;
    [SerializeField] private float pulseDuration = 0.4f;

    private InputSystemActions _controls;
    private Projectile _currentProjectile;
    private ChargeScaler _currentScaler;

    private void Awake()
    {
        _controls = new InputSystemActions();
    }

    private void OnEnable()
    {
        _controls.Player.Tap.started += OnTapStarted;
        _controls.Player.Tap.canceled += OnTapCanceled;
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Tap.started -= OnTapStarted;
        _controls.Player.Tap.canceled -= OnTapCanceled;
        _controls.Disable();
    }

    private void OnTapStarted(InputAction.CallbackContext ctx)
    {
        var pos = Pointer.current.position.ReadValue();
        var ray = cam.ScreenPointToRay(pos);

        if (!Physics.Raycast(ray, out var hit, 2000f)) return;
        _currentProjectile = Instantiate(projectilePrefab, hit.point, Quaternion.identity);

        _currentScaler = new ChargeScaler(
            _currentProjectile,
            playerBall,
            minScale,
            maxScale,
            pulseDuration
        );

        _currentScaler.StartPulse();
    }

    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        if (_currentProjectile == null)
            return;

        _currentScaler.StopPulse();
        _currentProjectile.Launch();

        _currentProjectile = null;
        _currentScaler = null;
    }
}