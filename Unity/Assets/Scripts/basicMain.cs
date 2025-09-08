using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.Experimental.GraphView;

public class basicMain : MonoBehaviour
{
    public Button Hello;                    
    public string host;                     //IP �ּ� (���÷��� 127.0.0.1)
    public int port;                        // ��Ʈ �ּ� (3000������ express ����)
    public string route;                    // about �ּ�

    private void Start()
    {
        this.Hello.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/{2}", host, port, route);                          // url �ּҸ� �ϼ��Ѵ� (ex : 127.0.0.1:3000/about)
            Debug.Log(url);

            StartCoroutine(this.GetBasic(url, (raw) =>
            {
                Debug.LogFormat("{0}", raw);
            }));
        });
    }

    private IEnumerator GetBasic(string url, System.Action<string> callback)
    {
        var WebRequest = UnityWebRequest.Get(url);
        yield return WebRequest.SendWebRequest();

        if(WebRequest.result == UnityWebRequest.Result.ConnectionError                          // ��� ���� ���� �����϶�
            || WebRequest.result == UnityWebRequest.Result.ProtocolError)                       // �������� ���� �϶�
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʾƼ� ��� �Ұ�");                                 // ��� �ȵ� ���� ó�� �Ѵ�.
        }
        else
        {
            callback(WebRequest.downloadHandler.text);                                      // ��� �Ϸ� �ǰ� �ش� �ؽ�Ʈ�� �����´�
        }
    }
}
