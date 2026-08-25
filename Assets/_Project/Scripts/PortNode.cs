using UnityEngine;
using PiratesDream.Data;
using PiratesDream.Managers;
using PiratesDream.UI;

namespace PiratesDream.Map
{
    public class PortNode : MonoBehaviour
    {
        [Header("Liman Veri Bağlantısı")]
        public PortData portData;

        private void OnMouseDown()
        {
            if (portData == null)
            {
                Debug.LogError("Bu liman objesine PortData bağlanmamış!");
                return;
            }

            if (PlayerManager.Instance == null || PlayerManager.Instance.currentPort == null)
            {
                Debug.LogError("_Managers nesnesindeki PlayerManager bileşeninde Current Port boş!");
                return;
            }

            // Eğer oyuncu zaten bu limandaysa Liman Menüsünü aç
            if (PlayerManager.Instance.currentPort == portData)
            {
                if (PortUIManager.Instance != null)
                {
                    PortUIManager.Instance.OpenPortMenu(portData);
                }
                return;
            }

            // Başka bir limandaysa rota hesabı ve seyahat
            PortNode currentPortNode = FindCurrentPortNode();
            
            if (currentPortNode != null)
            {
                float distance = Vector2.Distance(currentPortNode.transform.position, transform.position) * 10f;
                float travelTime = NavigationManager.Instance.CalculateTravelTimeMinutes(distance, PlayerManager.Instance.currentShip);
                float fuelNeeded = NavigationManager.Instance.CalculateFuelConsumption(distance, PlayerManager.Instance.currentShip);

                Debug.Log($"<color=cyan>[ROTA HESABI]</color> {currentPortNode.portData.portName} -> {portData.portName} | Mesafe: {distance:F1} mil | Süre: {travelTime:F1} dk | Gerekli Erzak: {fuelNeeded:F1}");

                if (RouteDrawer.Instance != null)
                {
                    RouteDrawer.Instance.DrawRoute(currentPortNode.transform.position, transform.position);
                }

                if (TravelManager.Instance != null && !TravelManager.Instance.isTraveling)
                {
                    TravelManager.Instance.StartTravel(portData, currentPortNode.transform.position, transform.position, fuelNeeded);
                }
            }
            else
            {
                Debug.LogWarning("Mevcut durduğunuz limanın sahnedeki objesi bulunamadı!");
            }
        }

        private PortNode FindCurrentPortNode()
        {
#pragma warning disable CS0618
            PortNode[] allNodes = Object.FindObjectsByType<PortNode>();
#pragma warning restore CS0618
            
            foreach (var node in allNodes)
            {
                if (node.portData == PlayerManager.Instance.currentPort)
                    return node;
            }
            return null;
        }
    }
}