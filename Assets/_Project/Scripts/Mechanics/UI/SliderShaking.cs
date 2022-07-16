using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SliderShaking : MonoBehaviour
    {
        Slider slider;
        Vector3 originalPos;
        float x;
        float y;
        public Image fillArea;
        Color32 fillAreaColor;
        

        private void Start()
        {
            slider = GetComponent<Slider>();
            StartCoroutine(Shake());
            originalPos = transform.localPosition;
        }

        private void Update()
        {
            fillAreaColor = Color.HSVToRGB(Mathf.Round(slider.value / 100 * 115), 100, 100);
            fillArea.color = fillAreaColor;
            Debug.Log(fillArea.color);
        }

        IEnumerator Shake ()
        {
            Vector3 originalPos = transform.localPosition;

            while(true)
            {
                float x = Random.Range(-1f, 1f) * slider.value / 6;
                float y = Random.Range(-1f, 1f) * slider.value / 6;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                yield return null;
            }
        }
        
    }
}
