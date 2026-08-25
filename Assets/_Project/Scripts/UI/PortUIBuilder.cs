using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PiratesDream.UI
{
    public class PortUIBuilder : MonoBehaviour
    {
        private void Start()
        {
            BuildPortUIPanel();
        }

        public void BuildPortUIPanel()
        {
            Canvas canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null) return;

            if (canvas.transform.Find("PortMenuPanel") != null) return;

            // 1. Ana Panel Arka Planı
            GameObject panelObj = new GameObject("PortMenuPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            panelObj.transform.SetParent(canvas.transform, false);

            RectTransform panelRect = panelObj.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = new Vector2(500, 400);

            Image panelImg = panelObj.GetComponent<Image>();
            panelImg.color = new Color(0.1f, 0.12f, 0.18f, 0.95f);

            // 2. Liman Başlığı & Açıklama
            GameObject titleObj = CreateTextObject("Txt_PortTitle", panelObj.transform, new Vector2(0, 150), new Vector2(450, 50), 28, Color.gold, TextAlignmentOptions.Center);
            GameObject descObj = CreateTextObject("Txt_PortDesc", panelObj.transform, new Vector2(0, 80), new Vector2(440, 80), 16, Color.white, TextAlignmentOptions.Center);

            // 3. Butonlar
            GameObject btnSupply = CreateButton("Btn_RefillSupplies", panelObj.transform, new Vector2(0, 0), "Erzak İkmali Yap (2 Altın/Birim)", new Color(0.15f, 0.45f, 0.65f));
            GameObject btnMarket = CreateButton("Btn_OpenMarket", panelObj.transform, new Vector2(0, -60), "Pazar Yerine Gir", new Color(0.2f, 0.6f, 0.3f));
            GameObject btnClose = CreateButton("Btn_ClosePort", panelObj.transform, new Vector2(0, -120), "Kapat", new Color(0.7f, 0.2f, 0.2f));

            // PortUIManager Atamaları
            PortUIManager manager = Object.FindAnyObjectByType<PortUIManager>();
            if (manager != null)
            {
                manager.portMenuPanel = panelObj;
                manager.portTitleText = titleObj.GetComponent<TextMeshProUGUI>();
                manager.portDescriptionText = descObj.GetComponent<TextMeshProUGUI>();

                btnSupply.GetComponent<Button>().onClick.AddListener(manager.OnRefillSuppliesClicked);
                btnMarket.GetComponent<Button>().onClick.AddListener(manager.OnMarketButtonClicked);
                btnClose.GetComponent<Button>().onClick.AddListener(manager.ClosePortMenu);
            }

            panelObj.SetActive(false);
            Debug.Log("<color=green>[UI BUILDER]</color> Liman Menü Paneli otomatik oluşturuldu.");
        }

        private GameObject CreateTextObject(string name, Transform parent, Vector2 pos, Vector2 size, float fontSize, Color color, TextAlignmentOptions align)
        {
            GameObject textObj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            textObj.transform.SetParent(parent, false);

            RectTransform rect = textObj.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;
            rect.sizeDelta = size;

            TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
            tmp.text = name;
            tmp.fontSize = fontSize;
            tmp.color = color;
            tmp.alignment = align;

            return textObj;
        }

        private GameObject CreateButton(string name, Transform parent, Vector2 pos, string label, Color btnColor)
        {
            GameObject btnObj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            btnObj.transform.SetParent(parent, false);

            RectTransform rect = btnObj.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;
            rect.sizeDelta = new Vector2(380, 45);

            Image img = btnObj.GetComponent<Image>();
            img.color = btnColor;

            GameObject textObj = CreateTextObject("Text", btnObj.transform, Vector2.zero, new Vector2(380, 45), 18, Color.white, TextAlignmentOptions.Center);
            textObj.GetComponent<TextMeshProUGUI>().text = label;

            return btnObj;
        }
    }
}