using UnityEngine;
using PiratesDream.Data;

namespace PiratesDream.Managers
{
    public class MarketManager : MonoBehaviour
    {
        public static MarketManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        /// <summary>
        /// Mevcut limandan mal satın alma işlemi.
        /// </summary>
        public bool BuyItem(MarketItem item, int amount)
        {
            if (item == null || !item.isAvailable)
            {
                Debug.LogWarning("Bu ürün bu limanda mevcut değil!");
                return false;
            }

            int totalCost = item.basePrice * amount;

            // Altın kontrolü
            if (PlayerManager.Instance.currentGold < totalCost)
            {
                Debug.LogWarning("Yetersiz altın!");
                return false;
            }

            // Kargo kapasitesi kontrolü
            bool added = PlayerManager.Instance.AddCargo(item.itemName, amount, item.basePrice);
            if (added)
            {
                PlayerManager.Instance.SpendGold(totalCost);
                Debug.Log($"<color=green>[TİCARET]</color> {amount} adet {item.itemName} satın alındı. Harcanan: {totalCost} Altın.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Envanterdeki malı limana satma işlemi.
        /// </summary>
        public bool SellItem(string itemName, int amount, int sellPricePerUnit)
        {
            var cargoItem = PlayerManager.Instance.cargoHold.Find(x => x.itemName == itemName);

            if (cargoItem == null || cargoItem.amount < amount)
            {
                Debug.LogWarning("Envanterinizde satacak yeterli miktarda bu maldan yok!");
                return false;
            }

            int totalEarned = sellPricePerUnit * amount;

            cargoItem.amount -= amount;
            if (cargoItem.amount <= 0)
            {
                PlayerManager.Instance.cargoHold.Remove(cargoItem);
            }

            PlayerManager.Instance.AddGold(totalEarned);
            Debug.Log($"<color=green>[TİCARET]</color> {amount} adet {itemName} satıldı. Kazanılan: {totalEarned} Altın.");
            return true;
        }
    }
}