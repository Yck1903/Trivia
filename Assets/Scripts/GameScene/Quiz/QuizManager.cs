using DG.Tweening;
using Enums;
using Trivia.GameScene.UI;
using UnityEngine;

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
        EventManager.Subscribe(GameEvents.OnCountdownFinished, ProcessTimeIsUp);
    }

    private void OnDisable()
    {
        EventManager<string>.Unsubscribe(GameEvents.OnAnswerButtonClicked, CheckAnswer);
        EventManager.Unsubscribe(GameEvents.OnCountdownFinished, ProcessTimeIsUp);
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

        _quizUiManager.QuestionUI.EnableAnswerButtons(false);
        
        Sequence seq = DOTween.Sequence();
        
        if (currentQuestion.Answer == givenAnswer)
        {
            seq.Append(_quizUiManager.OnCorrectAnswer(givenAnswer, _questionPointSo.CorrectAnswerPoint));
        }
        else
        {
            seq.Append(_quizUiManager.OnWrongAnswer(currentQuestion.Answer, givenAnswer, _questionPointSo.WrongAnswerPoint));
        }

        seq.AppendCallback(EndQuestion);
    }

    private void ProcessTimeIsUp()
    {
        QuestionData currentQuestion = _questionGenerator.GetCurrentQuestion();
        
        Sequence seq = DOTween.Sequence();

        seq.Append(_quizUiManager.OnTimeIsUp(currentQuestion.Answer, _questionPointSo.TimeIsUpPoint));
        seq.AppendCallback(EndQuestion);
    }

    private void EndQuestion()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_quizUiManager.EndQuestion());
        seq.AppendCallback(StartQuestion);
    }
}