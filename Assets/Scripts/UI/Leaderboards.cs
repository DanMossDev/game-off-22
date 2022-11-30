using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Leaderboards : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] names;
    [SerializeField] TextMeshProUGUI[] scores;
    [SerializeField] TMP_InputField username;
    [SerializeField] TextMeshProUGUI errorMessage;

    string baseURL = "http://13.42.22.85/";

    void OnEnable()
    {
        GetHighscores();
    }

    public async void GetHighscores()
    {
        errorMessage.text = "";
        string url = baseURL + "getall";

        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        webRequest.SetRequestHeader("Content-Type", "application/json");

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone) await Task.Yield();

        if (webRequest.result == UnityWebRequest.Result.Success) {
            Highscore[] highscores = Highscore.CreateFromJSON(webRequest.downloadHandler.text);

            for (int i = 0; i < 10; i++)
            {
                if (i >= highscores.Length) break;

                names[i].text = highscores[i].name;
                scores[i].text = highscores[i].score.ToString();
            }
        }
        else if (webRequest.result == UnityWebRequest.Result.ConnectionError) errorMessage.text = "Couldn't connect to server.";
        else errorMessage.text = "Sorry, something went wrong!";
    }

    public async void PostHighscore()
    {
        errorMessage.text = "";
        if (username.text == "") {
            errorMessage.text = "Please enter a name for the leaderboards";
            return;
        }
        string url = baseURL + "add?name=" + username.text + "&score=" + ScoreHolder.Score;

        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone) await Task.Yield();

        if (webRequest.result == UnityWebRequest.Result.Success) {
            ShowHighscores.Instance.ToggleSubmit();
        }
        else if (webRequest.result == UnityWebRequest.Result.ConnectionError) errorMessage.text = "Couldn't connect to server.";
        else if (webRequest.result == UnityWebRequest.Result.DataProcessingError) errorMessage.text = "Incorrectly formatted username.";
        else errorMessage.text = "Sorry, something went wrong!";
    }
}


[Serializable]
public class Highscore
{
    public string name;
    public int score;

    public static Highscore[] CreateFromJSON(string jsonString)
    {
        return JsonHelper.FromJson<Highscore>("{\"Items\":" + jsonString + "}");
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}