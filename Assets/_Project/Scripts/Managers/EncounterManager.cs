using System.Collections.Generic;
using UnityEngine;
using PiratesDream.UI;

namespace PiratesDream.Managers
{
    [System.Serializable]
    public class SeaEvent
    {
        public string title;
        public string description;
        public string option1Text;
        public string option2Text;
    }

    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager Instance { get; private set; }

        public List<SeaEvent> eventPool = new List<SeaEvent>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            InitializeDefaultEvents();
        }

        private void InitializeDefaultEvents()
        {
            eventPool.Add(new SeaEvent
            {
                title = "Terk Edilmiş Gemi Enkazı!",
                description = "Açık denizde yarı batık bir kalyon gördünüz. Ambarında ganimet olabilir ancak zaman kaybettirecektir.",
                option1Text = "Enkazı Yağmala",
                option2Text = "Yoluna Devam Et"
            });

            eventPool.Add(new SeaEvent
            {
                title = "Aniden Bastıran Fırtına!",
                description = "Gökyüzü karardı ve şiddetli bir fırtına geminizi vurmak üzere. Yelkenleri küçültüp fırtınayı aşmalı mısınız?",
                option1Text = "Fırtınaya Göğüs Ger",
                option2Text = "Güvenli Limana Sığın"
            });

            eventPool.Add(new SeaEvent
            {
                title = "Gözcü Çığlığı: Korsanlar!",
                description = "Ufukta siyah bayraklı hızlı bir şalupa belirdi. Üzerinize doğru hızla yaklaşıyorlar!",
                option1Text = "Topları Hazırla (Savaş)",
                option2Text = "Tam Yol Kaç"
            });

            eventPool.Add(new SeaEvent
            {
                title = "Denizci Filikası!",
                description = "Suda mahsur kalmış bir grup denizci yardım çığlıkları atıyor. Onları gemiye almak erzak stoklarınızı zorlayabilir.",
                option1Text = "Kazazedeleri Kurtar",
                option2Text = "Görmezden Gel"
            });
        }

        public void TriggerRandomEvent()
        {
            if (eventPool.Count == 0 || EncounterUIManager.Instance == null) return;

            int randomIndex = Random.Range(0, eventPool.Count);
            SeaEvent randomEvent = eventPool[randomIndex];

            EncounterUIManager.Instance.ShowEncounter(
                randomEvent.title,
                randomEvent.description,
                randomEvent.option1Text,
                randomEvent.option2Text
            );
        }
    }
}