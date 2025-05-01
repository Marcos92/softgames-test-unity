using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private CardPile startPile;
    [SerializeField] private CardPile endPile;
    [SerializeField] private GameObject finishLabel;

    void Awake()
    {
        SetupStartPile();
        StartCoroutine(MoveCards());

        endPile.OnCardAdded += CheckIfFinished;
    }

    private void SetupStartPile()
    {
        for (int i = 0; i < 144; i++)
        {
            Card card = Instantiate(cardPrefab);
            startPile.AddCard(card);
        }

        startPile.SetupCardPositions();
    }

    private IEnumerator MoveCards()
    {
        yield return new WaitForSeconds(1.0f);

        while (startPile.CardCount > 0)
        {
            startPile.MoveTopCardToPile(endPile);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void CheckIfFinished()
    {
        if (endPile.CardCount >= 144)
        {
            finishLabel.SetActive(true);
        }
    }
}
