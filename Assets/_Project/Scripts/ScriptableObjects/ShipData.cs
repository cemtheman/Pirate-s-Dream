using UnityEngine;

namespace PiratesDream.Data
{
    [CreateAssetMenu(fileName = "NewShipData", menuName = "Pirates Dream/Ship Data")]
    public class ShipData : ScriptableObject
    {
        [Header("Gemi Genel Bilgileri")]
        public string shipName = "Karakter Gemisi";
        public Sprite shipIcon;

        [Header("Gemi Hız Özellikleri")]
        public float maxSpeed = 10f;
        public float cruisingSpeed = 8f; // Seyir Hızı (Mil/Saat)

        [Header("Tüketim ve Dayanıklılık")]
        public float fuelConsumption = 3f; // Mil başına tüketilen erzak/yakıt miktarı
        public int maxHealth = 100;
        public int currentHealth = 100;
        
        [Header("Kapasite")]
        public int cargoCapacity = 100; // Ambar kapasitesi (Ton)
        public int maxCrew = 20;
    }
}