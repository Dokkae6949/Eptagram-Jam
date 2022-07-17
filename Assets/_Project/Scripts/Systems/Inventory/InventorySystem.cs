using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        public Dice[] inventory;
        public SODice[] diceData;
        public SODice emptyDice;

        public Button _rollButton;
        public Button _doneButton;

        [SerializeField] private int _maxDice = 8;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            ResetInventory();
        }

        #region CanvasControl =======================================================================================================================

        public void EnableInventorySystem()
        {
            ResetAbilityDice();
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }
        public void ApproveSelection()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            List<int> newCharacteristics = new List<int>();
            
            for (int i = 0; i < 4; i++)
            {

                if(inventory[i].isEmpty)
                {
                    newCharacteristics.Add(1);
                }
                else
                {
                    newCharacteristics.Add(inventory[i].currentFace);
                }
                
            }
            GameManager.Instance.SetCharacteristics(newCharacteristics);
            GameManager.Instance.OnCharacteristicsDistributed();

        }
        #endregion

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
        public void ResetAbilityDice()
        {
            if (inventory.Length < 3) return;

            for (int i = 0; i < 4; i++)
            {
                inventory[i].UpdateDiceData(emptyDice);
            }

            _rollButton.interactable = true;
            _doneButton.interactable = false;
        }
        public void ResetInventory()
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                inventory[i].UpdateDiceData(emptyDice);
            }
        }
        public void AddDice(int amount)
        {
            if (inventory.Length < 4) return;


            for (int i = 4; i < inventory.Length && amount > 0; i++)
            {
                if (!inventory[i].isEmpty) continue;

                inventory[i].UpdateDiceData(diceData[Random.Range(0, diceData.Length)]);
                inventory[i].currentFace = Random.Range(0, inventory[i].faces.Length);
                amount--;
                i = 3;
            }
            if (GetDiceInInventory() > _maxDice - 1)
            {
                Debug.Log(GetDiceInInventory());
                _rollButton.interactable = false;
                _doneButton.interactable = true;

                return;
            }
        }

        private int GetDiceInInventory()
        {
            int count = 0;

            for (int i = 0; i < inventory.Length; i++)
            {
                count += inventory[i].isEmpty ? 0 : 1;
            }

            return count;
        }
    }
}