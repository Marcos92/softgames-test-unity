using UnityEngine;
using UnityEngine.UI;

public class FireColorAnimation : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private float duration;
    private float elapsed = 0;

    private int currentIndex = 0;
    private int nextIndex = 1;

    private new ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!particleSystem.isEmitting)
            return;

        float percentage = elapsed / duration;
        Color color = Color.Lerp(colors[currentIndex], colors[nextIndex], percentage);

        var temp = particleSystem.main;
        temp.startColor = color;

        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            elapsed = 0;
            currentIndex = nextIndex;
            nextIndex = (nextIndex + 1) % colors.Length;
        }
    }

    public void ToggleOnOff()
    {
        if (particleSystem.isEmitting)
        {
            particleSystem.Stop();
        }
        else
        {
            particleSystem.Play();
        }
    }
}
