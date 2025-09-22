using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using System;

using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.Requests;
public class GameAPI : MonoBehaviour
{
    private string baseUrl = "http://localhost:4000/api";           // Node.js ������ URL

    // �÷��̾� ��������
    public IEnumerator RegisterPlayer(string playerName, string password)
    {
        var requestData = new { name = playerName, password = password };
        string jsonData = JsonConvert.SerializeObject(requestData);
        Debug.Log($"Registering player: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-type", "applicarion/json");

            yield return request.SendWebRequest();

            if(request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error registering player : {request.result}");
            }
            else
            {
                Debug.LogError($"Player registered successfully");
            }
        }
    }

    // �÷��̾� �α��� �޼���
    public IEnumerator LoginPlayer(string playerName, string password, Action<PlayerModel> onSuccecc)
    {
        var requestData = new {name = playerName, password = password};
        string jsonData = JsonConvert.SerializeObject(requestData);

        using (UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-type", "applicarion/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error registering player : {request.result}");
            }
            else
            {
                // ������ ó���Ͽ� PlayerModel ����
                string responseBody = request.downloadHandler.text;

                try
                {
                    var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

                    // ���� ���信�� PlayerModel ����
                    PlayerModel playerMode = new PlayerModel(responseData["playerName"].ToString())
                    {
                        metal = Convert.ToInt32(responseData["Metal"]),
                        crystal = Convert.ToInt32(responseData["crystal"]),
                        deuteriurm = Convert.ToInt32(responseData["deuteriurm"]),
                        planets = new List<PlayerModel>()
                    };
                    onSuccecc?.Invoke(playerMode);
                    Debug.Log("Login successful");
                }
                catch(Exception ex)
                {
                    Debug.LogError($"Error processing loin responce : {ex.Message}");
                }
            }
        }
    }
}
