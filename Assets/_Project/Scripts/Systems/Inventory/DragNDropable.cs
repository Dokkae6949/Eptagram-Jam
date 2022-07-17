using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Inventory
{
    [RequireComponent(typeof(RectTransform), typeof(Dice))]
    public class DragNDropable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _maxDropDistance;

        private RectTransform _rectTransform;
        private Dice _dice;
        private InventorySystem _inventorySystem;
        private Vector3 _startPosition;


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _inventorySystem = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<InventorySystem>();
            _dice = GetComponent<Dice>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = _rectTransform.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Dice dice = GetClosestDice();

            _rectTransform.position = _startPosition;

            if (dice == null) return;
            
            Face[] facesTMP = _dice.faces;
            int curFaceTMP = _dice.currentFace;
            bool isEmptyTMP = _dice.isEmpty;

            _dice.faces = dice.faces;
            _dice.currentFace = dice.currentFace;
            _dice.isEmpty = dice.isEmpty;

            dice.faces = facesTMP;
            dice.currentFace = curFaceTMP;
            dice.isEmpty = isEmptyTMP;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        private Dice GetClosestDice()
        {
            float closest = float.MaxValue;
            Dice closestDice = null;

            foreach (var dice in _inventorySystem.inventory)
            {
                if (_dice == dice) continue;

                float newDist = Vector2.Distance(transform.position, dice.transform.position);
                if (newDist < closest)
                {
                    closestDice = dice;
                    closest = newDist;
                }
            }

            if (closest > _maxDropDistance) return null;

            return closestDice;
        }
    }
}
