using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPile : MonoBehaviour
{
    private List<Card> cards = new();
    public int CardCount => cards.Count;

    [HideInInspector] public RectTransform rectTransform;

    public Action OnCardAdded;
    public Action OnCardRemoved;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
        card.transform.SetParent(transform, true);
        OnCardAdded.Invoke();
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        card.transform.SetParent(transform.root);
        OnCardRemoved.Invoke();
    }

    public void SetupCardPositions()
    {
        for (int i = 0; i < CardCount; i++)
        {
            cards[i].SetPosition(GetPositionAtIndex(i));
        }
    }

    public void MoveTopCardToPile(CardPile pile)
    {
        Card card = cards.Last();
        RemoveCard(card);
        card.MoveToPile(pile);
    }

    private Vector2 GetPositionAtIndex(int index)
    {
        return new Vector2(index * 0.25f, index * 0.25f);
    }

    public Vector2 GetTopPosition()
    {
        return GetPositionAtIndex(CardCount);
    }
}