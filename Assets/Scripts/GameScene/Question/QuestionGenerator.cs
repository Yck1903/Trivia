using UnityEngine;

public class QuestionGenerator
{
    private readonly QuizData _quizData;
    private int _currentQuestionIndex = -1;

    private int CurrentQuestionIndex
    {
        get => _currentQuestionIndex;
        set
        {
            _currentQuestionIndex = Mathf.Max(0, value);
            _currentQuestionIndex %= _quizData.Length;
        }
    }

    public QuestionGenerator(QuizData quizData)
    {
        _quizData = quizData;
    }

    public QuestionData GetNextQuestion()
    {
        CurrentQuestionIndex++;
        QuestionData questionData = GetCurrentQuestion();

        return questionData;
    }

    private QuestionData GetQuestion(int index)
    {
        return _quizData[index];
    }

    public QuestionData GetCurrentQuestion()
    {
        return GetQuestion(CurrentQuestionIndex);
    }
}