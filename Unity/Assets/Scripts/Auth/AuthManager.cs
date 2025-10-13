using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Networking;
using System.Data;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

public class AuthManager : MonoBehaviour
{
    // ���� URL �� PlayerPrefs Ű ��� ����
    private const string SERVER_URL = "http://localhost:4000";
    private const string ACCESS_TOKEN_PREFS_KEY = "accssToekn";
    private const string REFRESH_TOKEN_PREFS_KEY = "RefreshToekn";
    private const string TOLEN_EXPLRY_PREFS_KEY = "tokenExpiry";

    // ��ū �� ���� �ð� ���� ����
    private string accssToekn;
    private string refreshToken;
    private DateTime tokenExpiryTime;

    private void Start()
    {
        LoadTokenFromPrefs();
    }
    // PlayerPrefs ���� ��ū ���� �ε�
    private void LoadTokenFromPrefs()
    {
        accssToekn = PlayerPrefs.GetString(ACCESS_TOKEN_PREFS_KEY, accssToekn);
        refreshToken = PlayerPrefs.GetString(REFRESH_TOKEN_PREFS_KEY, refreshToken);
        long expiryTicks = Convert.ToInt64(PlayerPrefs.GetString(TOLEN_EXPLRY_PREFS_KEY, "0"));
        tokenExpiryTime = new DateTime(expiryTicks);
    }

    // Player Prefs �� ���� ����
    private void SaveTokenToPrefs(string accessToken, string refreshToken, DateTime expiryTime)
    {
        PlayerPrefs.SetString(ACCESS_TOKEN_PREFS_KEY, accessToken);
        PlayerPrefs.SetString(REFRESH_TOKEN_PREFS_KEY, refreshToken);
        PlayerPrefs.SetString(TOLEN_EXPLRY_PREFS_KEY, expiryTime.Ticks.ToString());

        this.accssToekn = accessToken;
        this.refreshToken = refreshToken;
        this.tokenExpiryTime = expiryTime;
    }

    // ����� ��� �ڷ�ƾ
    public IEnumerator Register(string username, string password)
    {
        var user = new {username, password};
        var jsonData = JsonConvert.SerializeObject(user);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm($"{SERVER_URL}/register", "POST"))
        {
            byte[] boayRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(boayRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Registration Error : {www.error}");
            }
            else
            {
                Debug.Log("Registration successful");
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        var user = new { username, password };
        var jsonData = JsonConvert.SerializeObject(user);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm($"{SERVER_URL}/register", "POST"))
        {
            byte[] boayRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(boayRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Registration Error : {www.error}");
            }
            else
            {
               var respone = JsonConvert.DeserializeObject<LoginResponse>(www.downloadHandler.text);
                Debug.Log(respone);
                SaveTokenToPrefs(respone.accessToekn, respone.refreshToken, DateTime.UtcNow.AddMinutes(15));
                Debug.Log("Login successful");
            }
        }
    }
}

// �α��� ���� ������ ����
[System.Serializable]
public class LoginResponse
{
    public string accessToekn;
    public string refreshToken;
}

// ��ū ���� ������ ����
[System.Serializable]
public class RefeshTokenResponse
{
    public string accessToekn;
}
