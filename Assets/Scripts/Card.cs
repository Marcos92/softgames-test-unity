using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField] private float animationDuration;
    [SerializeField] private AnimationCurve animationCurve;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector2 position)
    {
        rectTransform.anchoredPosition = position;
    }

    public void MoveToPile(CardPile pile)
    {
        StartCoroutine(MoveToPileCoroutine(pile));
    }

    private IEnumerator MoveToPileCoroutine(CardPile pile)
    {
        float elapsed = 0;
        float duration = animationDuration;

        Vector2 origin = rectTransform.anchoredPosition;
        Vector2 target = pile.rectTransform.anchoredPosition + pile.GetTopPosition();

        while (elapsed < duration)
        {
            float percentage = elapsed / duration;
            elapsed += Time.deltaTime;
            SetPosition(Vector2.Lerp(origin, target, animationCurve.Evaluate(percentage)));
            yield return null;
        }

        pile.AddCard(this);
    }
}