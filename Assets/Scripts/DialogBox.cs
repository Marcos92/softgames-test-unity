using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text dialogLabel;
    [SerializeField] private Image avatar;
    [SerializeField] private Sprite defaultAvatar;

    private RectTransform nameRect;
    private RectTransform dialogRect;
    private RectTransform avatarRect;

    void Awake()
    {
        nameRect = nameLabel.GetComponent<RectTransform>();
        dialogRect = dialogLabel.GetComponent<RectTransform>();
        avatarRect = avatar.GetComponent<RectTransform>();
    }

    public void UpdateDialog(string name, string dialog, Sprite sprite, string position)
    {
        nameLabel.text = name;
        dialogLabel.text = dialog;

        if (sprite == null)
        {
            avatar.sprite = defaultAvatar;
        }
        else
        {
            avatar.sprite = sprite;
        }

        UpdatePosition(position);
    }

    private void UpdatePosition(string position)
    {
        if (position == "left")
        {
            nameRect.anchoredPosition = new Vector2(-250, 100);
            dialogRect.anchoredPosition = new Vector2(150, -50);
            avatarRect.anchoredPosition = new Vector2(-550, 0);

            nameLabel.alignment = TextAlignmentOptions.Left;
            dialogLabel.alignment = TextAlignmentOptions.Left;
        }
        else
        {
            nameRect.anchoredPosition = new Vector2(250, 100);
            dialogRect.anchoredPosition = new Vector2(-150, -50);
            avatarRect.anchoredPosition = new Vector2(550, 0);

            nameLabel.alignment = TextAlignmentOptions.Right;
            dialogLabel.alignment = TextAlignmentOptions.Right;
        }
    }
}