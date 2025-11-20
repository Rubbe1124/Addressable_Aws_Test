using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameResult
{
    public string UserName;
    public int Score;
}
public class WebManager : MonoBehaviour
{
    private string _baseUrl = "https://localhost:44327/api";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameResult res = new GameResult()
        {
            UserName = "Rubbe",
            Score = 999
        };

        SendPostRequest("ranking", res, (uwr) =>
        {
            Debug.Log("TODO : UI 갱신하기");
        });
        
        SendGetAllRequest("ranking", (uwr) =>
        {
            Debug.Log("TODO : UI 갱신하기");
        });
    }
    
    public void SendPostRequest(string url,object obj, Action<UnityWebRequest> callback)
    =>StartCoroutine(CoSendWebRequest(url,"POST",obj,callback));
    
    public void SendGetAllRequest(string url, Action<UnityWebRequest> callback)
        =>StartCoroutine(CoSendWebRequest(url,"GET",null,callback));
    
    IEnumerator CoSendWebRequest(string url, string method,object obj, Action<UnityWebRequest> callback)
    {
        string sendUrl = $"{_baseUrl}/{url}/";
        
        // Json 형태로 보내주기

        byte[] jsonBytes = null;

        if (obj != null)
        {
            string jsonStr = JsonUtility.ToJson(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }
        
        var uwr = new UnityWebRequest(sendUrl, method);
        uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log(uwr.downloadHandler.text);
            callback.Invoke(uwr);
        }
    }
}
