using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PiratesDream.UI
{
    public class EncounterUIBuilder : MonoBehaviour
    {
        private void Start()
        {
            BuildEncounterPanel();
        }

        public void BuildEncounterPanel()
        {
            Canvas canvas = Object.FindAnyObjectByType<Canvas>();
            if (canvas == null) return;

            if (canvas.transform.Find("EncounterPanel") != null) return;

            // 1. Ana Panel Arka Planı
            GameObject panelObj = new GameObject("EncounterPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            panelObj.transform.SetParent(canvas.transform, false);

            RectTransform panelRect = panelObj.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0.5f, 0.5f);
            panelRect.anchorMax = new Vector2(0.5f, 0.5f);
            panelRect.pivot = new Vector2(0.5f, 0.5f);
            panelRect.sizeDelta = new Vector2(580, 420);

            Image panelImg = panelObj.GetComponent<Image>();
            panelImg.color = new Color(0.15f, 0.08f, 0.08f, 0.98f); // Koyu kırmızı/kahve korsan teması

            // 2. Metin Alanları
            GameObject titleObj = CreateText("Txt_EventTitle", panelObj.transform, new Vector2(0, 160), new Vector2(520, 40), "OLAY BAŞLIĞI", 26, Color.gold);
            GameObject descObj = CreateText("Txt_EventDesc", panelObj.transform, new Vector2(0, 80), new Vector2(500, 100), "Olay açıklaması burada görünecek.", 17, Color.white);
            GameObject resultObj = CreateText("Txt_EventResult", panelObj.transform, new Vector2(0, -20), new Vector2(500, 50), "", 16, Color.yellow);

            // 3. Seçenek Butonları
            GameObject btnOpt1 = CreateButton("Btn_Opt1", panelObj.transform, new Vector2(-130, -120), "Seçenek 1", new Color(0.2f, 0.5f, 0.3f));
            GameObject btnOpt2 = CreateButton("Btn_Opt2", panelObj.transform, new Vector2(130, -120), "Seçenek 2", new Color(0.6f, 0.2f, 0.2f));

            // Manager Bağlantıları
            EncounterUIManager manager = Object.FindAnyObjectByType<EncounterUIManager>();
            if (manager != null)
            {
                manager.encounterPanel = panelObj;
                manager.titleText = titleObj.GetComponent<TextMeshProUGUI>();
                manager.descriptionText = descObj.GetComponent<TextMeshProUGUI>();
                manager.resultText = resultObj.GetComponent<TextMeshProUGUI>();

                manager.option1ButtonText = btnOpt1.GetComponentInChildren<TextMeshProUGUI>();
                manager.option2ButtonText = btnOpt2.GetComponentInChildren<TextMeshProUGUI>();

                btnOpt1.GetComponent<Button>().onClick.AddListener(manager.OnOption1Selected);
                btnOpt2.GetComponent<Button>().onClick.AddListener(manager.OnOption2Selected);
            }

            panelObj.SetActive(false);
            Debug.Log("<color=green>[UI BUILDER]</color> Deniz Olayı Paneli otomatik oluşturuldu.");
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
            rect.sizeDelta = new Vector2(240, 50);

            Image img = obj.GetComponent<Image>();
            img.color = btnColor;

            GameObject textObj = CreateText("Text", obj.transform, Vector2.zero, new Vector2(240, 50), label, 16, Color.white);

            return obj;
        }
    }
}