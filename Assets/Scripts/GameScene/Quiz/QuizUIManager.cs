using DG.Tweening;
using UnityEngine;

namespace Trivia.GameScene.UI
{
    public class QuizUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private QuestionUI _questionUI;
        [SerializeField] private PointAreaUI _pointAreaUI;
        [SerializeField] private CountdownUI _countdownUI;

        public QuestionUI QuestionUI => _questionUI;
        public PointAreaUI PointAreaUI => _pointAreaUI;
        public CountdownUI CountdownUI => _countdownUI;

        public void Init()
        {
            SetCanvasCamera();

            _questionUI.Init();
            _pointAreaUI.Init();
            _countdownUI.Init();
        }
    
        private void OnDisable()
        {
            _questionUI.Destroy();
        }

        private void SetCanvasCamera()
        {
            _canvas.worldCamera = Camera.main;
        }
    
        public void StartQuestion(QuestionData questionData)
        {
            QuestionUI.SetQuestionTexts(questionData.Question, questionData.Choices);
            QuestionUI.BringQuestion();
            CountdownUI.StartTimer();
        }

        public Tween EndQuestion()
        {
            return QuestionUI.EndQuestion();
        }

        public Tween OnCorrectAnswer(string correctAnswer, int earnedPoint)
        {
            Sequence seq = DOTween.Sequence();
        
            seq.Append(QuestionUI.HighlightCorrectAnswer(correctAnswer));
            PointAreaUI.ChangePoints(true, earnedPoint);
            CountdownUI.ResetTimer();

            return seq;
        }

        public Tween OnWrongAnswer(string correctAnswer, string givenAnswer, int lostPoint)
        {
            Sequence seq = DOTween.Sequence();
        
            seq.Append(QuestionUI.HighlightWrongAnswer(correctAnswer, givenAnswer));
            PointAreaUI.ChangePoints(false, lostPoint);
            CountdownUI.ResetTimer();
        
            return seq;
        }
    
        public Tween OnTimeIsUp(string correctAnswer, int lostPoint)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(QuestionUI.HighlightTimeIsUp(correctAnswer));
        
            PointAreaUI.ChangePoints(false, lostPoint);
            CountdownUI.ResetTimer();

            return seq;
        }
    }
}