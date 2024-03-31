using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private Camera mainCamera;

    public Vector2 leftTop { get; private set; }
    public Vector2 rightTop { get; private set; }
    public Vector2 leftBottom { get; private set; }
    public Vector2 rightBottom { get; private set; }

    public int PositionCount => lineRenderer.positionCount;

    private void Awake()
    {
        mainCamera = Camera.main;

        leftTop = mainCamera.ScreenToWorldPoint(Vector2.up * Screen.height);
        rightTop = mainCamera.ScreenToWorldPoint(Vector2.right * Screen.width + Vector2.up * Screen.height);
        leftBottom = mainCamera.ScreenToWorldPoint(Vector2.zero);
        rightBottom = mainCamera.ScreenToWorldPoint(Vector2.right * Screen.width);
    }

    public Vector2 GetPositionByIndex(int index) => lineRenderer.GetPosition(index);
    public void AddPointMousePosition() => lineRenderer.SetPosition(++lineRenderer.positionCount - 1, new Vector2(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y));
    public void AddPoint(Vector2 position) => lineRenderer.SetPosition(++lineRenderer.positionCount - 1, position);
    public void AddPoint(float x, float y) => lineRenderer.SetPosition(++lineRenderer.positionCount - 1, new Vector2(x, y));
    public void Clear() => lineRenderer.positionCount = 0;
}
