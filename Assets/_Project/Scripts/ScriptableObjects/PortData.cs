using System.Collections.Generic;
using UnityEngine;

namespace PiratesDream.Data
{
    [System.Serializable]
    public class MarketItem
    {
        public string itemName;
        public int basePrice;
        public bool isAvailable;
    }

    [CreateAssetMenu(fileName = "NewPortData", menuName = "Pirate's Dream/Port Data")]
    public class PortData : ScriptableObject
    {
        [Header("Port Info")]
        public string portId;
        public string portName;

        [TextArea(2, 4)]
        public string portDescription;
        public Sprite portBanner;

        [Header("Coordinates")]
        public Vector2 mapCoordinates;

        [Header("Facilities")]
        public bool hasTavern = true;
        public bool hasShipyard = true;
        public bool hasMarket = true;

        [Header("Marketplace")]
        public List<MarketItem> localMarket;

        [Header("Quests")]
        public List<MysteryQuestData> availableQuestsInPort;
    }
}