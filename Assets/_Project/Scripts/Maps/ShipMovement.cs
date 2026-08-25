using UnityEngine;
using PiratesDream.Managers;

namespace PiratesDream.Map
{
    public class ShipMovement : MonoBehaviour
    {
        public static ShipMovement Instance { get; private set; }

        [Header("Görsel Ayarlar")]
        public SpriteRenderer spriteRenderer;

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private bool isMoving = false;
        private float travelDuration = 4f;
        private float elapsedTime = 0f;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (PlayerManager.Instance != null && PlayerManager.Instance.currentPort != null)
            {
                PortNode portNode = FindPortNode(PlayerManager.Instance.currentPort.portName);
                if (portNode != null)
                {
                    Vector3 pos = portNode.transform.position;
                    pos.z = 0f; // Z eksenini kameranın önüne sabitle
                    transform.position = pos;
                }
            }
        }

        private void Update()
        {
            if (!isMoving) return;

            if (TravelManager.Instance != null && !TravelManager.Instance.isTraveling) return;

            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / travelDuration);

            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, progress);
            currentPos.z = 0f;
            transform.position = currentPos;

            Vector3 direction = targetPosition - startPosition;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (progress >= 1f)
            {
                isMoving = false;
            }
        }

        public void MoveToTarget(Vector3 start, Vector3 target, float duration)
        {
            start.z = 0f;
            target.z = 0f;
            startPosition = start;
            targetPosition = target;
            travelDuration = duration;
            elapsedTime = 0f;
            transform.position = start;
            isMoving = true;
        }

        private PortNode FindPortNode(string portName)
        {
            PortNode[] nodes = Object.FindObjectsByType<PortNode>();
            foreach (var node in nodes)
            {
                if (node.portData != null && node.portData.portName == portName)
                {
                    return node;
                }
            }
            return null;
        }
    }
}