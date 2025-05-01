using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class DialogDataLoader : MonoBehaviour
{
    private DialogData data;
    private readonly string url = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
    private bool isDestroyed = false;

    public static Action<DialogData> OnDataLoaded;

    void Start()
    {
        StartCoroutine(LoadData());
    }

    void OnDestroy()
    {
        isDestroyed = true;
    }

    private IEnumerator LoadData()
    {
        int maxRetries = 3;
        int attempts = 0;
        bool success = false;

        while (attempts < maxRetries && !success)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                success = true;
                string json = request.downloadHandler.text;
                data = JsonUtility.FromJson<DialogData>(json);

                if (data == null || data.avatars == null)
                {
                    Debug.LogError("Failed to parse DialogData or avatars array is missing.");
                    yield break;
                }

                foreach (var avatar in data.avatars)
                {
                    if (string.IsNullOrEmpty(avatar.url))
                    {
                        Debug.LogWarning($"Avatar '{avatar.name}' has an empty or missing URL, skipping download.");
                        continue;
                    }
                    yield return StartCoroutine(DownloadAvatar(avatar));
                }

                OnDataLoaded?.Invoke(data);
            }
            else
            {
                Debug.LogWarning($"Attempt {attempts++} failed: {request.error}");
            }
        }

        if (!success)
        {
            Debug.LogError("Failed to load data after multiple attempts.");
        }
    }

    private IEnumerator DownloadAvatar(Avatar avatar)
    {
        if (isDestroyed) yield break;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(avatar.url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Failed to load avatar for '{avatar.name}': {request.error}");
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            avatar.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}

[Serializable]
public class DialogData
{
    public Dialogue[] dialogue;
    public Avatar[] avatars;
}

[Serializable]
public class Dialogue
{
    public string name;
    public string text;
}

[Serializable]
public class Avatar
{
    public string name;
    public string url;
    public string position;

    [NonSerialized]
    public Sprite sprite;
}
