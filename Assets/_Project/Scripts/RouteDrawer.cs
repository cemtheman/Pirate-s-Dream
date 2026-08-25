using UnityEngine;

namespace PiratesDream.Map
{
    [RequireComponent(typeof(LineRenderer))]
    public class RouteDrawer : MonoBehaviour
    {
        public static RouteDrawer Instance { get; private set; }

        private LineRenderer lineRenderer;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            lineRenderer = GetComponent<LineRenderer>();
            ConfigureLineRenderer();
        }

        private void ConfigureLineRenderer()
        {
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.15f;
            lineRenderer.positionCount = 0;

            // Çizgiyi geminin bir tık arkasına çekiyoruz (Z = 1)
            lineRenderer.sortingOrder = 1; 

            // Basit sarı materyal ayarı
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.yellow;
            lineRenderer.endColor = Color.yellow;
        }

        public void DrawRoute(Vector3 start, Vector3 end)
        {
            // Çizginin Z derinliğini 1 yapıyoruz
            start.z = 1f;
            end.z = 1f;

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

        public void ClearRoute()
        {
            lineRenderer.positionCount = 0;
        }
    }
}