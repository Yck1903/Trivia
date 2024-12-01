using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizLoader _quizLoader;
    [SerializeField] private QuizUIManager _quizUiManagerPrefab;
    [SerializeField] private QuestionPointSo _questionPointSo;
    
    private QuestionGenerator _questionGenerator;
    private QuizUIManager _quizUiManager;
    
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

        _quizUiManager = Instantiate(_quizUiManagerPrefab);
        _quizUiManager.Init();

        StartQuestion();
    }

    private void StartQuestion()
    {
        QuestionData currentQuestion = _questionGenerator.GetNextQuestion();
        _quizUiManager.StartQuestion(currentQuestion);
    }

    private void CheckAnswer(string givenAnswer)
    {
        QuestionData currentQuestion = _questionGenerator.GetCurrentQuestion();

        if (currentQuestion.Answer == givenAnswer)
        {
            _quizUiManager.OnCorrectAnswer(givenAnswer, _questionPointSo.CorrectAnswerPoint);
        }
        else
        {
            _quizUiManager.OnWrongAnswer(currentQuestion.Answer, givenAnswer, _questionPointSo.WrongAnswerPoint);
        }

        EndQuestion();
    }

    private void EndQuestion()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_quizUiManager.EndQuestion());
        seq.AppendCallback(StartQuestion);
    }
}