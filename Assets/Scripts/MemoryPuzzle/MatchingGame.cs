using UnityEngine;
using System.Collections;

public class MatchingGameManager : MonoBehaviour
{
    private Card firstCard = null;
    private Card secondCard = null;
    public GameObject crystalToDestroy;

    private bool busy = false;
    public int totalPairs = 3;
    private int matchedPairs = 0;

    public void InitializeGame()
    {
        firstCard = null;
        secondCard = null;
        matchedPairs = 0;
        busy = false;

        Card[] cards = GetComponentsInChildren<Card>(true);
        foreach (Card card in cards)
        {
            card.gameObject.SetActive(true);
            card.CloseCard();
        }
    }

    public bool IsBusy() => busy;

    public void OnCardSelected(Card card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null && card != firstCard)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        busy = true;

        // ⏳ תני ליוניטי הזדמנות להציג את הקלף השני!
        yield return new WaitForSeconds(0.5f);

        if (firstCard.GetCardTag() == secondCard.GetCardTag())
        {
            matchedPairs++;

            firstCard.gameObject.SetActive(false);
            secondCard.gameObject.SetActive(false);

            if (matchedPairs >= totalPairs)
            {
                if (crystalToDestroy != null)
                    Destroy(crystalToDestroy);

                gameObject.SetActive(false);
            }
        }
        else
        {
            yield return new WaitForSeconds(1f); // אפשרי: זמן נוסף לפני סגירה
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
        busy = false;
    }

}
