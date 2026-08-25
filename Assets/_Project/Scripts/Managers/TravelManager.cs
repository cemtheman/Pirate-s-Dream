using System.Collections;
using UnityEngine;
using PiratesDream.Data;
using PiratesDream.UI;
using PiratesDream.Map;

namespace PiratesDream.Managers
{
    public class TravelManager : MonoBehaviour
    {
        public static TravelManager Instance { get; private set; }

        [Header("Seyahat Durumu")]
        public bool isTraveling = false;
        public float travelSpeedMultiplier = 1f;

        private bool eventTriggeredThisTrip = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartTravel(PortData targetPort, Vector3 startPos, Vector3 endPos, float fuelNeeded)
        {
            if (isTraveling) return;

            StartCoroutine(TravelRoutine(targetPort, startPos, endPos, fuelNeeded));
        }

        private IEnumerator TravelRoutine(PortData targetPort, Vector3 startPos, Vector3 endPos, float fuelNeeded)
        {
            isTraveling = true;
            eventTriggeredThisTrip = false;

            float duration = 4f / travelSpeedMultiplier;

            // Gemi görsel hareketini başlat
            if (ShipMovement.Instance != null)
            {
                ShipMovement.Instance.MoveToTarget(startPos, endPos, duration);
            }

            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.currentPort = null;
                PlayerManager.Instance.ConsumeSupplies(fuelNeeded);
                if (UIManager.Instance != null) UIManager.Instance.UpdateDashboardUI();
            }

            Debug.Log($"<color=yellow>[SEYAHAT BAŞLADI]</color> Hedef: {targetPort.portName}");

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                while (!isTraveling)
                {
                    yield return null;
                }

                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;

                if (progress >= 0.5f && !eventTriggeredThisTrip)
                {
                    eventTriggeredThisTrip = true;
                    
                    if (EncounterManager.Instance != null)
                    {
                        EncounterManager.Instance.TriggerRandomEvent();
                    }
                }

                yield return null;
            }

            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.currentPort = targetPort;
                if (UIManager.Instance != null) UIManager.Instance.UpdateDashboardUI();
            }

            if (RouteDrawer.Instance != null)
            {
                RouteDrawer.Instance.ClearRoute();
            }

            isTraveling = false;
            Debug.Log($"<color=green>[VARIŞ TAMAMLANDI]</color> Yeni Liman: {targetPort.portName}");
        }
    }
}