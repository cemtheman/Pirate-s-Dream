using System.Collections.Generic;
using UnityEngine;
using PiratesDream.Data;

namespace PiratesDream.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [Header("Oyuncu Durumu")]
        public string playerName = "Kaptan";
        public int currentGold = 1000;
        public float currentSupplies = 100f;
        public float maxSupplies = 100f;

        [Header("Mevcut Konum ve Gemi")]
        public PortData currentPort;
        public ShipData currentShip;

        [Header("Kargo Ambarı")]
        public List<CargoItem> cargoHold = new List<CargoItem>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            // Eğer oyun başladığında liman seçili değilse sahnedeki ilk limanı otomatik atar
            if (currentPort == null)
            {
                var firstPortNode = Object.FindAnyObjectByType<PiratesDream.Map.PortNode>();
                if (firstPortNode != null && firstPortNode.portData != null)
                {
                    currentPort = firstPortNode.portData;
                    Debug.Log($"<color=cyan>[BAŞLANGIÇ LİMANI]</color> {currentPort.portName} olarak ayarlandı.");
                }
            }
        }

        public void AddGold(int amount)
        {
            currentGold += amount;
            Debug.Log($"<color=yellow>[ALTIN EKLE]</color> +{amount} Altın. Toplam: {currentGold}");
        }

        public bool SpendGold(int amount)
        {
            if (currentGold >= amount)
            {
                currentGold -= amount;
                Debug.Log($"<color=yellow>[ALTIN HARCA]</color> -{amount} Altın. Kalan: {currentGold}");
                return true;
            }
            
            Debug.LogWarning("Yetersiz altın!");
            return false;
        }

        public void ConsumeSupplies(float amount)
        {
            currentSupplies = Mathf.Max(0f, currentSupplies - amount);
            Debug.Log($"<color=cyan>[ERZAK TÜKETİMİ]</color> -{amount:F1} Erzak. Kalan: {currentSupplies:F1}");
        }

        public void RefillSupplies(float amount)
        {
            currentSupplies = Mathf.Min(maxSupplies, currentSupplies + amount);
            Debug.Log($"<color=cyan>[ERZAK İKMALİ]</color> +{amount:F1} Erzak. Toplam: {currentSupplies:F1}");
        }

        public int GetCurrentCargoWeight()
        {
            int totalWeight = 0;
            foreach (var item in cargoHold)
            {
                totalWeight += item.amount;
            }
            return totalWeight;
        }

        public bool AddCargo(string itemName, int amount, int buyPrice)
        {
            int maxCapacity = currentShip != null ? currentShip.cargoCapacity : 0;
            int currentCargoWeight = GetCurrentCargoWeight();

            if (currentCargoWeight + amount > maxCapacity)
            {
                Debug.LogWarning("Ambar kapasitesi yetersiz!");
                return false;
            }

            var existingItem = cargoHold.Find(x => x.itemName == itemName);
            if (existingItem != null)
            {
                existingItem.amount += amount;
            }
            else
            {
                cargoHold.Add(new CargoItem { itemName = itemName, amount = amount, purchasePrice = buyPrice });
            }

            Debug.Log($"<color=green>[AMBAR]</color> {amount} adet {itemName} ambara eklendi.");
            return true;
        }
    }

    [System.Serializable]
    public class CargoItem
    {
        public string itemName;
        public int amount;
        public int purchasePrice;
    }
}