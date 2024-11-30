using System;
using DG.Tweening;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizLoader _quizLoader;
    [SerializeField] private QuizViewManager _quizViewManagerPrefab;

    private QuestionGenerator _questionGenerator;
    private QuizViewManager _quizViewManager;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        EventManager<string>.Subscribe(GameEvents.OnAnswerButtonClicked, CheckAnswer);
    }

    private void OnDisable()
    {
        EventManager<string>.Unsubscribe(GameEvents.OnAnswerButtonClicked, CheckAnswer);
    }

    private void Init()
    {
        _questionGenerator = new QuestionGenerator(_quizLoader.GetQuizData());

        _quizViewManager = Instantiate(_quizViewManagerPrefab);
        _quizViewManager.Init();

        StartQuestion();
    }

    private void StartQuestion()
    {
        QuestionData currentQuestion = _questionGenerator.GetNextQuestion();
        _quizViewManager.StartQuestion(currentQuestion);
    }

    private void CheckAnswer(string givenAnswer)
    {
        QuestionData currentQuestion = _questionGenerator.GetCurrentQuestion();

        if (currentQuestion.Answer == givenAnswer)
        {
            _quizViewManager.OnCorrectAnswer(givenAnswer);
        }
        else
        {
            _quizViewManager.OnWrongAnswer(currentQuestion.Answer, givenAnswer);
        }

        EndQuestion();
    }

    private void EndQuestion()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_quizViewManager.EndQuestion());
        seq.AppendCallback(StartQuestion);
    }
}