using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Camera cam;

    private InputSystemActions _controls;
    private Projectile _currentProjectile;

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
        var screenPos = Pointer.current.position.ReadValue();

        var ray = cam.ScreenPointToRay(screenPos);

        if (!Physics.Raycast(ray, out var hit, 2000f)) return;
        _currentProjectile = Instantiate(projectilePrefab, hit.point, Quaternion.identity);
        _currentProjectile.StartCharging();
    }

    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        if (_currentProjectile == null) return;

        _currentProjectile.Launch();
        _currentProjectile = null;
    }
}