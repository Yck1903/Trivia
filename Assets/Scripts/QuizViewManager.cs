using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuizViewManager : MonoBehaviour
{
    [SerializeField] private QuizUIManager _quizUIManagerPrefab;
    private QuizUIManager _quizUIManager;

    public void Init()
    {
        _quizUIManager = Instantiate(_quizUIManagerPrefab);
        _quizUIManager.Init();
    }

    public void StartQuestion(QuestionData questionData)
    {
        _quizUIManager.QuestionUI.SetQuestionTexts(questionData.Question, questionData.Choices);
        _quizUIManager.QuestionUI.BringQuestion();
    }

    public Tween EndQuestion()
    {
        return _quizUIManager.QuestionUI.EndQuestion();
    }

    public void OnCorrectAnswer(string correctAnswer)
    {
        _quizUIManager.QuestionUI.HighlightCorrectAnswer(correctAnswer);
    }

    public void OnWrongAnswer(string correctAnswer, string givenAnswer)
    {
        _quizUIManager.QuestionUI.HighlightWrongAnswer(correctAnswer, givenAnswer);
    }
}