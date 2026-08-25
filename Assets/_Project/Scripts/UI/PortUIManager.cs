using UnityEngine;
using TMPro;
using PiratesDream.Data;
using PiratesDream.Managers;

namespace PiratesDream.UI
{
    public class PortUIManager : MonoBehaviour
    {
        public static PortUIManager Instance { get; private set; }

        [Header("UI Panelleri")]
        public GameObject portMenuPanel;

        [Header("Liman Başlık & Bilgi")]
        public TextMeshProUGUI portTitleText;
        public TextMeshProUGUI portDescriptionText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void OpenPortMenu(PortData port)
        {
            if (port == null) return;

            if (portTitleText != null) portTitleText.text = port.portName;
            if (portDescriptionText != null) portDescriptionText.text = port.portDescription;

            if (portMenuPanel != null) portMenuPanel.SetActive(true);
            Debug.Log($"<color=cyan>[LİMAN MENÜSÜ]</color> {port.portName} menüsü açıldı.");
        }

        public void ClosePortMenu()
        {
            if (portMenuPanel != null) portMenuPanel.SetActive(false);
        }

        public void OnRefillSuppliesClicked()
        {
            if (PlayerManager.Instance == null) return;

            float missingSupplies = PlayerManager.Instance.maxSupplies - PlayerManager.Instance.currentSupplies;
            if (missingSupplies <= 0)
            {
                Debug.Log("Erzak ambarınız zaten tamamen dolu!");
                return;
            }

            int cost = Mathf.CeilToInt(missingSupplies * 2f);

            if (PlayerManager.Instance.SpendGold(cost))
            {
                PlayerManager.Instance.RefillSupplies(missingSupplies);
                if (UIManager.Instance != null) UIManager.Instance.UpdateDashboardUI();
                Debug.Log($"<color=green>[İKMAL]</color> {cost} altın harcanarak erzak dolduruldu.");
            }
        }

        public void OnMarketButtonClicked()
        {
            ClosePortMenu();
            if (MarketUIManager.Instance != null)
            {
                MarketUIManager.Instance.OpenMarketUI();
            }
            else
            {
                Debug.LogError("MarketUIManager sahnedeki _Managers nesnesinde bulunamadı!");
            }
        }
    }
}