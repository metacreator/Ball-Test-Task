using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private PlayerBall playerBall;

    [SerializeField] private Transform doorTarget;
    [SerializeField] private LineRenderer line;

    [Header("Settings")] [SerializeField] private float lineHeight = 0.01f;
    [SerializeField] private float fadeDistance = 30f;

    private Material _mat;
    private static readonly int FadeEnd = Shader.PropertyToID("_FadeEnd");
    private static readonly int FadeStart = Shader.PropertyToID("_FadeStart");

    private void Awake()
    {
        _mat = line.material;
        BuildPath();
        RefreshPath();

        playerBall.OnScaleChanged += HandlePlayerScaleChanged;
    }

    private void OnDestroy()
    {
        playerBall.OnScaleChanged -= HandlePlayerScaleChanged;
    }

    private void HandlePlayerScaleChanged(float newScale)
    {
        UpdateWidth();
    }

    private void Update()
    {
        UpdatePathPositions();
    }

    private void UpdatePathPositions()
    {
        if (!playerBall || !doorTarget) return;

        var dist = Vector3.Distance(playerBall.transform.position, doorTarget.position);

        if (dist <= 3f)
        {
            if (line.gameObject.activeSelf)
                line.gameObject.SetActive(false);

            return;
        }

        if (!line.gameObject.activeSelf)
            line.gameObject.SetActive(true);

        var start = playerBall.transform.position;
        var end = doorTarget.position;

        start.y = lineHeight;
        end.y = lineHeight;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        UpdateFade();
    }


    private void BuildPath()
    {
        if (playerBall == null || doorTarget == null)
            return;

        line.useWorldSpace = true;

        var start = playerBall.transform.position;
        var end = doorTarget.position;

        start = new Vector3(start.x, lineHeight, start.z);
        end = new Vector3(end.x, lineHeight, end.z);

        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    private void RefreshPath()
    {
        UpdateWidth();
        UpdateFade();
    }

    private void UpdateWidth()
    {
        var size = playerBall.CurrentScale;

        var curve = new AnimationCurve();
        curve.AddKey(0f, size);
        curve.AddKey(1f, size);

        line.widthCurve = curve;
    }

    private void UpdateFade()
    {
        var start = line.GetPosition(0);
        var end = line.GetPosition(1);

        var dist = Vector3.Distance(start, end);
        var fadeStart = Mathf.Clamp01((dist - fadeDistance) / dist);

        _mat.SetFloat(FadeStart, fadeStart);
        _mat.SetFloat(FadeEnd, 1f);
    }
}