using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Trivia.GameScene.Buttons;
using UnityEngine;

namespace Trivia.GameScene.UI
{
    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _questionBox;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private AnswerButton _answerButtonPrefab;
        [SerializeField] private TextMeshProUGUI _answerFeedback;
        [SerializeField] private TextMeshProUGUI _questionNumberText;

        private List<AnswerButton> _answerButtons;
        private const float _answerButtonsSpacingDistance = 150;
        private const float _questionBoxAndAnswerButtonPadding = 150;

        private int _currentQuestionNumber = 0;

        public void Init()
        {
            GenerateAnswerButtons();
        }

        public void Destroy()
        {
            foreach (var answerButton in _answerButtons)
            {
                answerButton.Unsubscribe();
            }
        }

        private void GenerateAnswerButtons()
        {
            _answerButtons = new List<AnswerButton>();

            var questionBoxAnchoredPosition = _questionBox.anchoredPosition;
            Vector2 anchorPos = new Vector2(questionBoxAnchoredPosition.x, questionBoxAnchoredPosition.y - (_questionBox.sizeDelta.y / 2 + _questionBoxAndAnswerButtonPadding));

            foreach (var answer in QuizData.Answers)
            {
                AnswerButton answerButton = Instantiate(_answerButtonPrefab, transform);
                answerButton.SetAnswer(answer);
                answerButton.SetAnchorPos(anchorPos);
                answerButton.EnableClick(false);
                answerButton.Subscribe();
                anchorPos.y -= _answerButtonsSpacingDistance;

                _answerButtons.Add(answerButton);
            }
        }

        public void EnableAnswerButtons(bool enable)
        {
            foreach (var answerButton in _answerButtons)
            {
                answerButton.EnableClick(enable);
            }
        }

        public void SetQuestionTexts(string questionText, IReadOnlyList<string> choices)
        {
            _questionText.SetText(questionText);

            for (int i = 0; i < choices.Count; i++)
            {
                _answerButtons[i].SetAnswerText(choices[i]);
            }
        }

        public void BringQuestion()
        {
            SetQuestionNumberText();
            
            Sequence seq = DOTween.Sequence();

            seq.Append(_questionBox.DOScale(1f, 0.4f).From(0).SetEase(Ease.OutBack));
            for (var i = 0; i < _answerButtons.Count; i++)
            {
                AnswerButton answerButton = _answerButtons[i];
                seq.Join(answerButton.transform.DOScale(1f, 0.3f).From(0).SetEase(Ease.OutBack, 2.5f).SetDelay((i + 1) * 0.04f));
            }

            seq.AppendCallback(() => EnableAnswerButtons(true));
        }

        public Tween EndQuestion()
        {
            Sequence seq = DOTween.Sequence();

            seq.AppendInterval(0.1f);
            seq.AppendCallback(() => EnableAnswerButtons(false));
            seq.Append(_questionBox.DOScale(0f, 0.2f).SetEase(Ease.OutSine));
            for (var i = 0; i < _answerButtons.Count; i++)
            {
                AnswerButton answerButton = _answerButtons[i];
                seq.Join(answerButton.transform.DOScale(0f, 0.2f).SetEase(Ease.OutSine).SetDelay((i + 1) * 0.01f));
            }

            seq.AppendCallback(ResetAnswerColors);

            return seq;
        }

        public Tween HighlightCorrectAnswer(string answer)
        {
            foreach (var answerButton in _answerButtons)
            {
                if (answerButton.Answer == answer)
                {
                    answerButton.SetAnswerColor(true);
                }
            }

            return GiveCorrectAnswerFeedback();
        }

        public Tween HighlightWrongAnswer(string correctAnswer, string givenAnswer)
        {
            foreach (var answerButton in _answerButtons)
            {
                if (answerButton.Answer == correctAnswer)
                {
                    answerButton.SetAnswerColor(true);
                }

                if (answerButton.Answer == givenAnswer)
                {
                    answerButton.SetAnswerColor(false);
                }
            }

            return GiveWrongAnswerFeedback();
        }

        public Tween HighlightTimeIsUp(string correctAnswer)
        {
            foreach (var answerButton in _answerButtons)
            {
                if (answerButton.Answer == correctAnswer)
                {
                    answerButton.SetAnswerColor(true);
                }
            }

            return GiveTimeIsUpFeedback();
        }

        private Tween GiveCorrectAnswerFeedback()
        {
            _answerFeedback.color = Color.green;
            _answerFeedback.text = "EXCELLENT";
            _answerFeedback.gameObject.SetActive(true);
        
            return ShowAnswerFeedback(0.4f);
        }

        private Tween GiveWrongAnswerFeedback()
        {
            _answerFeedback.color = Color.red;
            _answerFeedback.text = "BUMMER";
            _answerFeedback.gameObject.SetActive(true);

            return ShowAnswerFeedback(0.4f);
        }
    
        private Tween GiveTimeIsUpFeedback()
        {
            _answerFeedback.color = Color.red;
            _answerFeedback.text = "TIME IS UP!";
            _answerFeedback.gameObject.SetActive(true);
        
            return ShowAnswerFeedback(1f);
        }

        private void SetQuestionNumberText()
        {
            _currentQuestionNumber++;
            _questionNumberText.text = "Question Number: " + _currentQuestionNumber;
        }

        private Tween ShowAnswerFeedback(float displayTime)
        {
            Sequence seq = DOTween.Sequence();
        
            seq.Append(_answerFeedback.transform.DOScale(1f, 0.25f).From(0).SetEase(Ease.OutBack));
            seq.AppendInterval(displayTime);
            seq.AppendCallback(() => _answerFeedback.transform.DOScale(0f, 0.25f).SetEase(Ease.InBack));

            return seq;
        }

        private void ResetAnswerColors()
        {
            foreach (var answerButton in _answerButtons)
            {
                answerButton.ResetColor();
            }
        }
    }
}