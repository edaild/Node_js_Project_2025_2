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
    // 서버 URL 및 PlayerPrefs 키 상수 정의
    private const string SERVER_URL = "http://localhost:4000";
    private const string ACCESS_TOKEN_PREFS_KEY = "accssToekn";
    private const string REFRESH_TOKEN_PREFS_KEY = "RefreshToekn";
    private const string TOLEN_EXPLRY_PREFS_KEY = "tokenExpiry";

    // 토큰 및 만료 시간 저장 변수
    private string accssToekn;
    private string refreshToken;
    private DateTime tokenExpiryTime;

    private void Start()
    {
        LoadTokenFromPrefs();
    }
    // PlayerPrefs 에서 토큰 정보 로드
    private void LoadTokenFromPrefs()
    {
        accssToekn = PlayerPrefs.GetString(ACCESS_TOKEN_PREFS_KEY, accssToekn);
        refreshToken = PlayerPrefs.GetString(REFRESH_TOKEN_PREFS_KEY, refreshToken);
        long expiryTicks = Convert.ToInt64(PlayerPrefs.GetString(TOLEN_EXPLRY_PREFS_KEY, "0"));
        tokenExpiryTime = new DateTime(expiryTicks);
    }

    // Player Prefs 에 정보 저장
    private void SaveTokenToPrefs(string accessToken, string refreshToken, DateTime expiryTime)
    {
        PlayerPrefs.SetString(ACCESS_TOKEN_PREFS_KEY, accessToken);
        PlayerPrefs.SetString(REFRESH_TOKEN_PREFS_KEY, refreshToken);
        PlayerPrefs.SetString(TOLEN_EXPLRY_PREFS_KEY, expiryTime.Ticks.ToString());

        this.accssToekn = accessToken;
        this.refreshToken = refreshToken;
        this.tokenExpiryTime = expiryTime;
    }

    // 사용자 등록 코루틴
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

// 로그인 응답 데이터 구조
[System.Serializable]
public class LoginResponse
{
    public string accessToekn;
    public string refreshToken;
}

// 토큰 갱신 데이터 구조
[System.Serializable]
public class RefeshTokenResponse
{
    public string accessToekn;
}
