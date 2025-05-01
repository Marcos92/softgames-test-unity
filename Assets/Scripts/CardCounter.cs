using TMPro;
using UnityEngine;

public class CardCounter : MonoBehaviour
{
    private TMP_Text label;
    private CardPile pile;

    void Awake()
    {
        label = GetComponent<TMP_Text>();
        pile = GetComponentInParent<CardPile>();
        pile.OnCardAdded += UpdateLabel;
        pile.OnCardRemoved += UpdateLabel;
    }

    void OnDestroy()
    {
        pile.OnCardAdded -= UpdateLabel;
        pile.OnCardRemoved -= UpdateLabel;
    }

    private void UpdateLabel()
    {
        label.text = pile.CardCount.ToString();
    }
}
