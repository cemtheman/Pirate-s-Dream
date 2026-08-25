using UnityEngine;

namespace PiratesDream.Data
{
    public enum EncounterType { MysteryClue, WeatherCondition, PirateAttack, MerchantTrade }

    [System.Serializable]
    public class EventOption
    {
        public string optionText;       // Örn: "Sandığı sudan çıkar", "Yoluna devam et"
        [TextArea(2, 4)]
        public string resultText;       // Seçim sonrası gösterilecek metin
        public int goldReward;          // Kazanılacak/kaybedilecek altın (+/-)
        public float hullDamage;        // Gemiye verilecek hasar
        public Clue unlockedClue;       // Eğer bu seçim bir ipucu kazandırıyorsa
    }

    [CreateAssetMenu(fileName = "NewEncounterEvent", menuName = "Pirate's Dream/Encounter Event Data")]
    public class EncounterEventData : ScriptableObject
    {
        public string eventId;
        public string eventTitle;
        public EncounterType type;
        
        [TextArea(3, 6)]
        public string eventDescription; // Olayın başlangıç hikaye metni
        public Sprite eventImage;

        public EventOption optionA;
        public EventOption optionB;
    }
}