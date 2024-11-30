using UnityEngine;

public class QuizUIManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private QuestionUI _questionUI;

    public QuestionUI QuestionUI => _questionUI;

    public void Init()
    {
        SetCanvasCamera();

        _questionUI.Init();
    }

    private void OnDisable()
    {
        _questionUI.Destroy();
    }

    private void SetCanvasCamera()
    {
        _canvas.worldCamera = Camera.main;
    }
}