using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text fpsText;
    private readonly float hudRefreshRate = 1f;

    private float timer;

    void Awake()
    {
        fpsText = GetComponent<TMP_Text>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps + " FPS";
            timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}
