using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InfectionHandler infectionHandler;
    [SerializeField] private PlayerBall playerBall;
    [SerializeField] private Transform doorTarget;

    [SerializeField] private float spawnDistance = 0.6f;

    [Header("Charge Settings")] [SerializeField]
    private float minScale = 0.15f;

    [SerializeField] private float maxScale = 0.35f;
    [SerializeField] private float chargeSpeed = 0.3f;

    private InputSystemActions _controls;
    private Projectile _currentProjectile;
    private ChargeScaler _chargeScaler;

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

    private void Update()
    {
        _chargeScaler?.Tick(Time.deltaTime);
    }

    private void OnTapStarted(InputAction.CallbackContext ctx)
    {
        var dir = (doorTarget.position - playerBall.transform.position).normalized;
        var spawnPos = playerBall.transform.position + dir * spawnDistance;
        spawnPos.y = playerBall.transform.position.y;

        _currentProjectile = infectionHandler.SpawnProjectile(spawnPos, dir);

        _chargeScaler = new ChargeScaler(
            _currentProjectile,
            playerBall,
            minScale,
            maxScale,
            chargeSpeed
        );
        _chargeScaler.StartCharging();
    }


    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        _chargeScaler?.StopCharging();
        _currentProjectile?.Launch();

        _chargeScaler = null;
        _currentProjectile = null;
    }
}