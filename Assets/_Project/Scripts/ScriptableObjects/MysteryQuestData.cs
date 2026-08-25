using System.Collections.Generic;
using UnityEngine;

namespace PiratesDream.Data
{
    public enum QuestStatus { Locked, Active, Completed }

    [System.Serializable]
    public class Clue
    {
        public string clueId;
        public string clueTitle;
        [TextArea(2, 5)]
        public string clueDescription;
        public bool isUnlocked = false;
    }

    [CreateAssetMenu(fileName = "NewMysteryQuest", menuName = "Pirate's Dream/Quest Data")]
    public class MysteryQuestData : ScriptableObject
    {
        [Header("Görev & Gizem Detayları")]
        public string questId;
        public string questTitle;
        [TextArea(3, 8)]
        public string questStoryText;
        public QuestStatus status = QuestStatus.Locked;

        [Header("İpuçları & Gereksinimler")]
        public List<Clue> requiredClues; // Bu gizemi çözmek için gereken ipucu zinciri

        [Header("Ödüller")]
        public int rewardGold;
        public string rewardTreasureItem; // Özel hazine eşyası veya harita parçası

        [Header("Sonraki Adım")]
        public MysteryQuestData nextQuestInChain; // Gizemin bir sonraki halkası
    }
}