using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [System.Serializable]
    public struct Face
    {
        public int number;
        public Sprite sprite;
        public Color color;
    }

    [CreateAssetMenu(fileName = "Dice Data", menuName = "Dice System/New Dice")]
    public class SODice : ScriptableObject
    {
        public Face[] faces;
        public int currentFace;
        public bool isEmpty;
    }
}
