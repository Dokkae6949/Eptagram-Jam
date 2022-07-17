using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        public Dice[] inventory;
        public SODice[] diceData;
        public SODice emptyDice;

        private int _nonEmptyDiceCount; // not the abilities


        public void SwapDice(int a, int b)
        {
            if (a == b) return;
            if (a == -1 || b == -1) return;
            if (a < 0 || a >= inventory.Length) return;
            if (b < 0 || b >= inventory.Length) return;

            Dice tmp = inventory[a];
            inventory[a] = inventory[b];
            inventory[b] = tmp;
        }
        public void ResetDice()
        {
            if (inventory.Length < 3) return;

            _nonEmptyDiceCount -= 4;
            for (int i = 0; i < 3; i++)
            {
                inventory[i].UpdateDiceData(emptyDice);
            }
        }
        public void AddDice(int amount)
        {
            while (amount > 0 && _nonEmptyDiceCount < 8)
            {
                for (int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i].isEmpty) continue;

                    inventory[i].UpdateDiceData(diceData[Random.Range(0, diceData.Length)]);
                }

                _nonEmptyDiceCount++;
                amount--;
            }
        }
    }
}