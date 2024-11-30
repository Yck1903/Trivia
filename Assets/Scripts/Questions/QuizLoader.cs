using Newtonsoft.Json;
using UnityEngine;

public class QuizLoader : MonoBehaviour
{
    [SerializeField] private TextAsset _quizDataJson;
    private QuizData _quizData;

    private QuizData LoadQuiz()
    {
        return JsonConvert.DeserializeObject<QuizData>(_quizDataJson.text);
    }

    public QuizData GetQuizData()
    {
        return _quizData ??= LoadQuiz();
    }
}