using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InfectionHandler infectionHandler;
    [SerializeField] private PlayerBall playerBall;
    [SerializeField] private Transform doorTarget;
    [SerializeField] private Game game;

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

        _chargeScaler = new ChargeScaler(playerBall, minScale, maxScale, chargeSpeed);
        _chargeScaler.OnPlayerTooSmall += HandlePlayerTooSmall;
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
        _chargeScaler.Tick(Time.deltaTime);
    }

    private void OnTapStarted(InputAction.CallbackContext ctx)
    {
        var screenPos = _controls.Player.TapPosition.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(screenPos);

        var plane = new Plane(Vector3.up, new Vector3(0, playerBall.transform.position.y, 0));

        float enter;
        if (!plane.Raycast(ray, out enter))
            return;

        var hitPoint = ray.GetPoint(enter);

        var dir = (hitPoint - playerBall.transform.position);
        dir.y = 0; 
        dir.Normalize();

        var spawnPos = playerBall.transform.position + dir * spawnDistance;
        spawnPos.y = playerBall.transform.position.y;

        _currentProjectile = infectionHandler.SpawnProjectile(spawnPos, dir);

        _chargeScaler.InitializeProjectile(_currentProjectile);

        _chargeScaler.StartCharging();
    }


    private void OnTapCanceled(InputAction.CallbackContext ctx)
    {
        _chargeScaler.StopCharging();
        _currentProjectile?.Launch();

        _currentProjectile = null;
    }

    private void HandlePlayerTooSmall()
    {
        game.Fail("Player too small to shoot");
    }
}