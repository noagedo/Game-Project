using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TriviaManager : MonoBehaviour
{
    public GameObject triviaPanel;
    public Text questionText;
    public Text feedbackText;
    public Button[] answerButtons;
    public Transform teleportLocation;

    private Action onCorrectCallback;
    private Action onWrongCallback;

    private bool questionAnswered = false;

    void Start()
    {
        if (triviaPanel != null)
            triviaPanel.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";
    }

    public void ShowQuestion(Action onCorrect, Action onWrong)
    {
        if (questionAnswered) return;

      
        if (questionText == null) { Debug.LogError("❌ questionText is null!"); return; }
        if (feedbackText == null) { Debug.LogError("❌ feedbackText is null!"); return; }
        if (answerButtons == null || answerButtons.Length < 3) { Debug.LogError("❌ answerButtons missing!"); return; }
        if (triviaPanel == null) { Debug.LogError("❌ triviaPanel is null!"); return; }

        onCorrectCallback = onCorrect;
        onWrongCallback = onWrong;

        triviaPanel.SetActive(true);
        questionText.text = "What is the number of Goblins in scene 1?";

        answerButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = "4";
        answerButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "5";
        answerButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "6";

        foreach (Button btn in answerButtons)
            btn.onClick.RemoveAllListeners();

        answerButtons[0].onClick.AddListener(() => Answer(false));
        answerButtons[1].onClick.AddListener(() => Answer(false));
        answerButtons[2].onClick.AddListener(() => Answer(true));
    }

    void Update()
    {
        if (!triviaPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            answerButtons[0].onClick.Invoke();

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            answerButtons[1].onClick.Invoke();

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            answerButtons[2].onClick.Invoke();
    }

    void Answer(bool isCorrect)
    {
        if (feedbackText == null) return;

        if (isCorrect)
        {
            feedbackText.text = "Correct Answer!";
            triviaPanel.SetActive(false);
            questionAnswered = true;
            onCorrectCallback?.Invoke();
        }
        else
        {
            feedbackText.text = "Wrong answer, teleporting...";
            onWrongCallback?.Invoke();
            triviaPanel.SetActive(false);

            questionAnswered = false; // <--- הוסף את זה
        }

    }

}
