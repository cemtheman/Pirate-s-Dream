using UnityEngine;
using TMPro;
using PiratesDream.Managers;

namespace PiratesDream.UI
{
    public class EncounterUIManager : MonoBehaviour
    {
        public static EncounterUIManager Instance { get; private set; }

        [Header("UI Paneli")]
        public GameObject encounterPanel;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI resultText;

        [Header("Buton Metinleri")]
        public TextMeshProUGUI option1ButtonText;
        public TextMeshProUGUI option2ButtonText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void ShowEncounter(string title, string description, string opt1Text, string opt2Text)
        {
            if (encounterPanel != null)
            {
                encounterPanel.SetActive(true);
                if (titleText != null) titleText.text = title;
                if (descriptionText != null) descriptionText.text = description;
                if (resultText != null) resultText.text = ""; // Sonuç metnini temizle

                if (option1ButtonText != null) option1ButtonText.text = opt1Text;
                if (option2ButtonText != null) option2ButtonText.text = opt2Text;

                // Olay esnasında seyahati duraklat
                if (TravelManager.Instance != null)
                {
                    TravelManager.Instance.isTraveling = false;
                }

                Debug.Log($"<color=orange>[DENİZ OLAYI]</color> {title} olayı tetiklendi!");
            }
        }

        public void OnOption1Selected()
        {
            // Örnek Seçenek 1: Riskli Karar (Örn: Enkazı Ara)
            int rewardGold = Random.Range(100, 300);
            PlayerManager.Instance.AddGold(rewardGold);
            
            if (resultText != null) 
                resultText.text = $"Başarılı! Ambarınız için {rewardGold} Altın değerinde ganimet buldunuz.";

            Invoke(nameof(CloseAndResumeTravel), 2f);
        }

        public void OnOption2Selected()
        {
            // Örnek Seçenek 2: Güvenli Karar (Örn: Pas Geç)
            if (resultText != null) 
                resultText.text = "Riski almayarak yolunuza güvenle devam ettiniz.";

            Invoke(nameof(CloseAndResumeTravel), 1.5f);
        }

        private void CloseAndResumeTravel()
        {
            if (encounterPanel != null) encounterPanel.SetActive(false);

            // Seyahati kaldığı yerden devam ettir
            if (TravelManager.Instance != null)
            {
                TravelManager.Instance.isTraveling = true;
            }

            if (UIManager.Instance != null) UIManager.Instance.UpdateDashboardUI();
        }
    }
}