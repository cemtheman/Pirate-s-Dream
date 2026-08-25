using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PiratesDream.UI
{
    public class MarketUIBuilder : MonoBehaviour
    {
        private void Start()
        {
            BuildMarketPanel();
        }

        public void BuildMarketPanel()
        {
            Canvas canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null) return;

            // Zaten oluşturulmuşsa tekrar oluşturma
            Transform existingPanel = canvas.transform.Find("MarketPanel");
            if (existingPanel != null)
            {
                BindToManager(existingPanel.gameObject);
                return;
            }

            // 1. Ana Pazar Paneli
            GameObject panelObj = new GameObject("MarketPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            panelObj.transform.SetParent(canvas.transform, false);

            RectTransform panelRect = panelObj.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = new Vector2(550, 450);

            Image panelImg = panelObj.GetComponent<Image>();
            panelImg.color = new Color(0.08f, 0.1f, 0.14f, 0.98f);

            // 2. Başlık ve Ambar Bilgisi
            GameObject titleObj = CreateText("Txt_MarketTitle", panelObj.transform, new Vector2(0, 180), new Vector2(500, 40), "LİMAN PAZAR YERİ", 26, Color.gold);
            GameObject cargoObj = CreateText("Txt_CargoInfo", panelObj.transform, new Vector2(0, 135), new Vector2(500, 30), "Ambar Doluluğu: 0 / 100 Ton", 18, Color.cyan);

            // 3. Örnek Ürün Satın Alma Butonları
            GameObject btnRum = CreateButton("Btn_BuyRum", panelObj.transform, new Vector2(0, 50), "Rom Satın Al (50 Altın)", new Color(0.2f, 0.5f, 0.3f));
            GameObject btnSpice = CreateButton("Btn_BuySpice", panelObj.transform, new Vector2(0, -10), "Baharat Satın Al (120 Altın)", new Color(0.6f, 0.4f, 0.1f));
            GameObject btnSilk = CreateButton("Btn_BuySilk", panelObj.transform, new Vector2(0, -70), "İpek Kumaş Satın Al (200 Altın)", new Color(0.5f, 0.2f, 0.5f));

            // 4. Kapat Butonu
            GameObject btnClose = CreateButton("Btn_CloseMarket", panelObj.transform, new Vector2(0, -150), "Pazardan Çık", new Color(0.7f, 0.2f, 0.2f));

            // Manager Bağlantıları
            MarketUIManager manager = Object.FindAnyObjectByType<MarketUIManager>();
            if (manager != null)
            {
                manager.marketPanel = panelObj;
                manager.marketTitleText = titleObj.GetComponent<TextMeshProUGUI>();
                manager.cargoInfoText = cargoObj.GetComponent<TextMeshProUGUI>();

                btnRum.GetComponent<Button>().onClick.AddListener(() => manager.BuySampleItem("Rom", 50));
                btnSpice.GetComponent<Button>().onClick.AddListener(() => manager.BuySampleItem("Baharat", 120));
                btnSilk.GetComponent<Button>().onClick.AddListener(() => manager.BuySampleItem("İpek Kumaş", 200));
                btnClose.GetComponent<Button>().onClick.AddListener(manager.CloseMarketUI);
            }

            panelObj.SetActive(false);
            Debug.Log("<color=green>[UI BUILDER]</color> Pazar Yeri Paneli oluşturuldu ve bağlandı.");
        }

        private void BindToManager(GameObject panelObj)
        {
            MarketUIManager manager = Object.FindAnyObjectByType<MarketUIManager>();
            if (manager != null && manager.marketPanel == null)
            {
                manager.marketPanel = panelObj;
            }
        }

        private GameObject CreateText(string name, Transform parent, Vector2 pos, Vector2 size, string content, float fontSize, Color color)
        {
            GameObject obj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            obj.transform.SetParent(parent, false);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;
            rect.sizeDelta = size;

            TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
            tmp.text = content;
            tmp.fontSize = fontSize;
            tmp.color = color;
            tmp.alignment = TextAlignmentOptions.Center;

            return obj;
        }

        private GameObject CreateButton(string name, Transform parent, Vector2 pos, string label, Color btnColor)
        {
            GameObject obj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            obj.transform.SetParent(parent, false);

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;
            rect.sizeDelta = new Vector2(420, 45);

            Image img = obj.GetComponent<Image>();
            img.color = btnColor;

            GameObject textObj = CreateText("Text", obj.transform, Vector2.zero, new Vector2(420, 45), label, 17, Color.white);

            return obj;
        }
    }
}