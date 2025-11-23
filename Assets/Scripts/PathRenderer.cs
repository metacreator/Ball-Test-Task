using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform playerBall;

    [SerializeField] private Transform doorTarget;
    [SerializeField] private LineRenderer line;

    [Header("Settings")] [SerializeField] private float lineHeight = 0.01f; // always above ground
    [SerializeField] private float fadeDistance = 30f;

    private Material mat;

    private void Awake()
    {
        mat = line.material;
        BuildPath();
        RefreshPath();
    }

    public void BuildPath()
    {
        if (playerBall == null || doorTarget == null)
            return;

        line.useWorldSpace = true;

        // Force horizontal path (flat at Y = lineHeight)
        Vector3 start = playerBall.position;
        Vector3 end = doorTarget.position;

        start = new Vector3(start.x, lineHeight, start.z);
        end = new Vector3(end.x, lineHeight, end.z);

        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    public void RefreshPath()
    {
        UpdateWidth();
        UpdateFade();
    }

    private void UpdateWidth()
    {
        float size = playerBall.localScale.x;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, size);
        curve.AddKey(1f, size);

        line.widthCurve = curve;
    }

    private void UpdateFade()
    {
        Vector3 start = line.GetPosition(0);
        Vector3 end = line.GetPosition(1);

        float dist = Vector3.Distance(start, end);
        float fadeStart = Mathf.Clamp01((dist - fadeDistance) / dist);

        mat.SetFloat("_FadeStart", fadeStart);
        mat.SetFloat("_FadeEnd", 1f);
    }
}