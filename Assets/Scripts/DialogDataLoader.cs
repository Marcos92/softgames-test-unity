using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class DialogDataLoader : MonoBehaviour
{
    private DialogData data;
    private readonly string url = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";

    public static Action<DialogData> OnDataLoaded;

    void Start()
    {
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");
            yield break;
        }

        string json = request.downloadHandler.text;
        data = JsonUtility.FromJson<DialogData>(json);

        foreach (var avatar in data.avatars)
        {
            yield return StartCoroutine(DownloadAvatar(avatar));
        }

        OnDataLoaded?.Invoke(data);
    }

    private IEnumerator DownloadAvatar(Avatar avatar)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(avatar.url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning($"Failed to load avatar for {avatar.name}: {request.error}");
        }
        else
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(request);
            avatar.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
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