using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameScene
{
    public class PointAreaUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private TextMeshProUGUI _pointAnimationText;

        private int _currentPoints;

        private Vector3 _animStartPos;
        
        public void Init()
        {
            _animStartPos = _pointAnimationText.transform.localPosition;
        }
        
        public void ResetPoints()
        {
            _currentPoints = 0;
        }

        private void ResetAnimationText()
        {
            _pointAnimationText.alpha = 0;
            _pointAnimationText.transform.localPosition = _animStartPos;
        }
        
        public void ChangePoints(bool isCorrectAnswer, int pointAmount)
        {
            _currentPoints += pointAmount;
            IncreasePointTextValue(_currentPoints - pointAmount, _currentPoints, 0.7f);
            
            PlayPointAnimation(isCorrectAnswer, pointAmount);
        }

        private void PlayPointAnimation(bool isCorrectAnswer, int pointAmount)
        {
            if (isCorrectAnswer)
            {
                _pointAnimationText.color = Color.green;
                _pointAnimationText.text = "+" + pointAmount;
            }
            else
            {
                _pointAnimationText.color = Color.red;
                _pointAnimationText.text = pointAmount.ToString();
            }
            
            Sequence seq = DOTween.Sequence();
            seq.Append(_pointAnimationText.transform.DOScale(1f, 0.7f).From(0).SetEase(Ease.OutBack));
            seq.Join(_pointAnimationText.transform.DOLocalMoveY(_animStartPos.y + 60, 0.7f).SetEase(Ease.OutSine));
            seq.Join(_pointAnimationText.DOFade(0.5f, 0.7f).From(0));
            seq.AppendInterval(0.1f);
            seq.AppendCallback(ResetAnimationText);
        }

        private void IncreasePointTextValue(int start, int target, float duration)
        {
            DOVirtual.Float(start, target, duration, value =>
            {
                _currentPoints = Mathf.RoundToInt(value);
                _pointsText.text = "Points: " + _currentPoints;
            }).OnComplete(() =>
            {
                _pointsText.text = "Points: " + target;
            });
        }
    }
}