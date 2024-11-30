using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _answerText;
    [SerializeField] private Button _answerButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Color _correctAnsweerColor;
    [SerializeField] private Color _wrongAnsweerColor;

    public string Answer { get; private set; }

    public void SetAnswer(string answer)
    {
        Answer = answer;
    }

    public void SetAnswerText(string text)
    {
        _answerText.SetText(text);
    }

    public void SetAnchorPos(Vector2 anchorPos)
    {
        _rectTransform.anchoredPosition = anchorPos;
    }

    public void EnableClick(bool click)
    {
        _answerButton.interactable = click;
    }

    public void SetAnswerColor(bool correct)
    {
        Color color = correct ? _correctAnsweerColor : _wrongAnsweerColor;
        SetButtonColor(color);
    }

    public void ResetColor()
    {
        SetButtonColor(Color.white);
    }

    private void SetButtonColor(Color color)
    {
        _answerButton.image.color = color;
    }

    public void Subscribe()
    {
        _answerButton.onClick.AddListener(OnAnswerClicked);
    }

    public void Unsubscribe()
    {
        _answerButton.onClick.RemoveAllListeners();
    }

    private void OnAnswerClicked()
    {
        EventManager<string>.Execute(GameEvents.OnAnswerButtonClicked, Answer);
    }
}