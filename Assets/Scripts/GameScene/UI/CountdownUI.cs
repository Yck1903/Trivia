using System.Collections;
using Enums;
using TMPro;
using UnityEngine;

namespace Trivia.GameScene.UI
{
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private QuestionCountdownTimeSo _questionCountdownTimeSo;
    
        private float _remainingTime;
        private bool _isRunning;

        private Vector3 _originalPosition;
        private Coroutine _shakeCoroutine;
        private Coroutine _countdownCoroutine;
    
        public void Init()
        {
            _originalPosition = _timerText.rectTransform.localPosition;
        }
    
        public void StartTimer()
        {
            if (_isRunning) return;
            _remainingTime = _questionCountdownTimeSo.QuestionCountdownTime;
            _countdownCoroutine = StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            _isRunning = true;
            while (_remainingTime > 0)
            {
                HandleVisualEffects();
                UpdateTimerUI();
                _remainingTime -= Time.deltaTime;
                yield return null;
            }

            _remainingTime = 0;
            UpdateTimerUI();
            _isRunning = false;

            EventManager.Execute(GameEvents.OnCountdownFinished);
        }

        private void UpdateTimerUI()
        {
            _timerText.text = Mathf.CeilToInt(_remainingTime).ToString();
        }
    
        private void HandleVisualEffects()
        {
            if (_remainingTime <= 5)
            {
                _timerText.color = Color.red;
                if (_shakeCoroutine == null)
                {
                    _shakeCoroutine = StartCoroutine(ShakeText());
                }
            }
            else if (_remainingTime <= 10)
            {
                _timerText.color = Color.yellow;
            }
            else
            {
                _timerText.color = Color.white;
            }
        }
    
        private IEnumerator ShakeText()
        {
            while (true)
            {
                Vector3 offset = Random.insideUnitCircle * 5f;
                _timerText.rectTransform.localPosition = _originalPosition + offset;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void ResetTimer()
        {
            if (_isRunning)
            {
                if (_countdownCoroutine != null)
                {
                    StopCoroutine(_countdownCoroutine);
                    _countdownCoroutine = null;
                }

                _isRunning = false;
            }

            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
                _shakeCoroutine = null;
                _timerText.rectTransform.localPosition = _originalPosition;
            }
        }
    }
}