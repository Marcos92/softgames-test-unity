using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private DialogData data;

    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private Button skipButton;

    private int currentIndex = 0;

    void Awake()
    {
        DialogDataLoader.OnDataLoaded += InitDialog;
        dialogBox.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        skipButton.onClick.AddListener(ShowNextDialog);
    }

    void OnDestroy()
    {
        DialogDataLoader.OnDataLoaded -= InitDialog;
        skipButton.onClick.RemoveListener(ShowNextDialog);
    }

    private void InitDialog(DialogData loadedData)
    {
        data = loadedData;
        dialogBox.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(true);
        ShowNextDialog();
    }

    private void ShowNextDialog()
    {
        string name = data.dialogue[currentIndex].name;
        string dialog = EmojiParser.Replace(data.dialogue[currentIndex].text);

        Avatar avatar = Array.Find(data.avatars, a => a.name == name);
        Sprite sprite = avatar?.sprite;
        string position = avatar?.position ?? "left";

        dialogBox.UpdateDialog(name, dialog, sprite, position);

        currentIndex++;

        if (currentIndex >= data.dialogue.Length)
        {
            skipButton.gameObject.SetActive(false);
        }
    }
}

public static class EmojiParser
{
    private static readonly Dictionary<string, string> emojiMap = new Dictionary<string, string>
    {
        { "affirmative", "👍" },
        { "intrigued", "🤔" },
        { "laughing", "😂" },
        { "neutral", "🙂" },
        { "satisfied", "😀" },
        { "win", "💪" }
    };

    public static string Replace(string text)
    {
        foreach (var emoji in emojiMap)
        {
            text = text.Replace($"{{{emoji.Key}}}", emoji.Value);
        }
        return text;
    }
}