using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // UnityWebRequest�� ����ϱ� ���� �ʿ�

public class APIClient : MonoBehaviour
{
    // API ��������Ʈ URL
    string apiUrl = "https://port-0-python-fastapi-17xco2nlsvi25ll.sel5.cloudtype.app/files/";

    void Start()
    {
        StartCoroutine(GetFileList("data")); // "data" ���͸��� ���� ����� �����ɴϴ�.
    }

    IEnumerator GetFileList(string directory)
    {
        string url = apiUrl + "?directory=" + directory; // ���� �Ķ���͸� ������ ��ü URL
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // ��û�� ������ ������ �� ������ ���
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // �����κ����� ���� ���
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
