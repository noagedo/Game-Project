using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public int cardKeyNumber; // מספר 1-6

    private MatchingGameManager manager;
    private bool isFlipped = false;

    void Start()
    {
        manager = FindObjectOfType<MatchingGameManager>();
        CloseCard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0 + cardKeyNumber))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        if (isFlipped || manager.IsBusy())
            return;

        OpenCard();
        manager.OnCardSelected(this);
    }

    public void OpenCard()
    {
        front.SetActive(true);
        back.SetActive(false);
        isFlipped = true;
    }

    public void CloseCard()
    {
        front.SetActive(false);
        back.SetActive(true);
        isFlipped = false;
    }

    public string GetCardTag()
    {
        return gameObject.tag;
    }
}
