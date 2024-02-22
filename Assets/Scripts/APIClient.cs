using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // UnityWebRequest를 사용하기 위해 필요

public class APIClient : MonoBehaviour
{
    // API 엔드포인트 URL
    string apiUrl = "https://port-0-python-fastapi-17xco2nlsvi25ll.sel5.cloudtype.app/files/";

    void Start()
    {
        StartCoroutine(GetFileList("data")); // "data" 디렉터리의 파일 목록을 가져옵니다.
    }

    IEnumerator GetFileList(string directory)
    {
        string url = apiUrl + "?directory=" + directory; // 쿼리 파라미터를 포함한 전체 URL
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // 요청을 보내고 응답이 올 때까지 대기
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // 서버로부터의 응답 출력
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
