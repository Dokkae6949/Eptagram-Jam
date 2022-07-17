using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class WIS_WaveInfoScreenManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private CanvasGroup _timerCanvasGroup;
        [SerializeField]
        private CanvasGroup _waveCanvasGroup;
        [SerializeField]
        private TextMeshProUGUI _timer;
        [SerializeField]
        private TextMeshProUGUI _waveText;
        [SerializeField]
        private List<Image> _monsterImages =new List<Image>();

        [Header("Prefabs")]

        [SerializeField]
        private Sprite _sprt_easy;
        [SerializeField]
        private Sprite _sprt_avarage;
        [SerializeField]
        private Sprite _sprt_hard;
        [SerializeField]
        private Sprite _sprt_hardcore;

        private bool _countdownEnabled;
        private float _countdownSeconds;
        private bool _nextSecond;
        private float _deltaSeconds;

        #region Initialization Methods ==============================================================================================================

        private void Start()
        {
            CountDown(10);
            _timerCanvasGroup.alpha = 0f;
            _waveCanvasGroup.alpha = 0f;
        }
        #endregion
        #region UpdateMethods =======================================================================================================================
        private void Update()
        {
            if (!_countdownEnabled) return;

            _countdownSeconds -= Time.deltaTime;
            if (_deltaSeconds - _countdownSeconds >= 1f)
            {
                Debug.Log(_countdownSeconds);
                _deltaSeconds = _countdownSeconds;
                ShowTimerDigit(Mathf.RoundToInt(_countdownSeconds));

            }
            if (_countdownSeconds <=0f)
            {
                _countdownEnabled = false;

                GameManager.Instance.OnCountdownOver();
            }

            





        }
        #endregion
        #region PublicMethods =======================================================================================================================

        public void CountDown(int seconds)
        {
            _countdownEnabled = true;
            _nextSecond = true;
            _countdownSeconds = seconds;
            _deltaSeconds = _countdownSeconds+1f;
        }
        public void SetWaveDifficultyInfo(List<int> monstersAmount)
        {
            int sum = 0;
            foreach (var item in monstersAmount)
            {
                sum += item;
            }

            foreach (var item2 in monstersAmount)
            {
                float percentage = (float)item2 / sum;
                switch (percentage)
                {
                    case <= 0.25f:
                        _monsterImages[0].sprite = _sprt_easy;
                        break;
                    case <= 0.50f:
                        _monsterImages[1].sprite = _sprt_avarage;
                        break;
                    case <= 0.75f:
                        _monsterImages[2].sprite = _sprt_hard;
                        break;
                    default:
                        _monsterImages[3].sprite = _sprt_hardcore;
                        break;
                }
            }
        }

        public void ShowTimerDigit(int time)
        {
            _timer.text = time.ToString();
            StartCoroutine(CanvasAnim(_timerCanvasGroup, 1f, 0f, 0.8f));
        }

        public void ShowWaveInfo(int number)
        {
            _waveText.text = $"Wave {number}";
            StartCoroutine(CanvasAnim(_waveCanvasGroup, _waveCanvasGroup.alpha, 1f, 0.5f));
        }

        public void HideWaveInfo()
        {
            StartCoroutine(CanvasAnim(_waveCanvasGroup, _waveCanvasGroup.alpha, 0f, 0.5f));
        }
        #endregion

        #region Coroutines ==========================================================================================================================

        private IEnumerator CanvasAnim(CanvasGroup canvasGroup,float start, float end, float lerpTime)
        {

            float startTime = Time.time;
            float workTime = 0f;
            float finalPosition = 0f;

            while (true)
            {
                workTime = Time.time - startTime;
                finalPosition = workTime / lerpTime;
                float currentValue = Mathf.Lerp(start, end, finalPosition);

                canvasGroup.alpha = currentValue;


                if (finalPosition >= 1)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion


    }
}
