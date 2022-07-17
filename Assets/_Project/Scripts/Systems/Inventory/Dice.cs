using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    [RequireComponent(typeof(RectTransform), typeof(RawImage))]
    public class Dice : MonoBehaviour
    {
        [SerializeField] private SODice _diceData;

        public Face[] faces;
        public int currentFace;
        public bool isEmpty;

        private RawImage _image;

        private void Start()
        {
            if (_image == null)
                _image = GetComponent<RawImage>();

            if (_diceData == null) return;

            UpdateDiceData();
        }
        private void Update()
        {
            if (currentFace < 0 || currentFace >= faces.Length) return;

            if (faces[currentFace].sprite != null)
                _image.texture = faces[currentFace].sprite.texture;

            _image.color = faces[currentFace].color;
        }

        public void UpdateDiceData(SODice data)
        {
            if (data == null) return;

            faces = data.faces;
            currentFace = data.currentFace;
            isEmpty = data.isEmpty;
        }
        public void UpdateDiceData()
        {
            if (_diceData == null) return;

            faces = _diceData.faces;
            currentFace = _diceData.currentFace;
            isEmpty = _diceData.isEmpty;
        }
    }
}