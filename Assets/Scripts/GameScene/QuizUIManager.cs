using DG.Tweening;
using GameScene;
using UnityEngine;

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
    }

    public Tween EndQuestion()
    {
        return QuestionUI.EndQuestion();
    }

    public void OnCorrectAnswer(string correctAnswer, int earnedPoint)
    {
        QuestionUI.HighlightCorrectAnswer(correctAnswer);
        PointAreaUI.ChangePoints(true, earnedPoint);
        // CountdownUI.ResetTimer();
    }

    public void OnWrongAnswer(string correctAnswer, string givenAnswer, int lostPoint)
    {
        QuestionUI.HighlightWrongAnswer(correctAnswer, givenAnswer);
        PointAreaUI.ChangePoints(false, lostPoint);
        // CountdownUI.ResetTimer();
    }
}