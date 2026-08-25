using UnityEngine;
using PiratesDream.Data;

namespace PiratesDream.Managers
{
    public class NavigationManager : MonoBehaviour
    {
        public static NavigationManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /// <summary>
        /// İki liman arasındaki kuş uçuşu mesafeyi hesaplar (Deniz mili cinsinden).
        /// </summary>
        public float CalculateDistance(PortData startPort, PortData targetPort)
        {
            if (startPort == null || targetPort == null) return 0f;
            
            // Map coordinates arası Mesafe x Ölçek Faktörü
            float distance = Vector2.Distance(startPort.mapCoordinates, targetPort.mapCoordinates);
            return distance * 10f; // 1 harita birimi = 10 Deniz Mili kabul edelim
        }

        /// <summary>
        /// Geminin hedef limana varış süresini hesaplar (Dakika cinsinden).
        /// </summary>
        public float CalculateTravelTimeMinutes(float distanceNauticalMiles, ShipData ship)
        {
            if (ship == null || ship.cruisingSpeed <= 0) return 0f;

            // Zaman = Yol / Hız
            float travelTimeHours = distanceNauticalMiles / ship.cruisingSpeed;
            return travelTimeHours * 60f; // Dakikaya çevir
        }

        /// <summary>
        /// Rota boyunca tüketilecek erzak/yakıt miktarını hesaplar.
        /// </summary>
        public float CalculateFuelConsumption(float distanceNauticalMiles, ShipData ship)
        {
            if (ship == null) return 0f;
            return distanceNauticalMiles * ship.fuelConsumption;
        }
    }
}