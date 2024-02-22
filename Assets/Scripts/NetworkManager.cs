using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;

[System.Serializable]
public class SignUpRequestData
{
    public string Login_ID;
    public string Login_Password;
}
[System.Serializable]
public class LoginRequestData
{
    public string Login_ID;
    public string Login_Password;
}

public class NetworkManager : MonoBehaviour
{
    // API 엔드포인트 URL
    public string[] apiUrls = {
        "http://127.0.0.1:8000",
        "https://port-0-python-fastapi-17xco2nlsvi25ll.sel5.cloudtype.app/files/",
    };

    public int selectApiUrlNum = 0;

    string apiUrl;

    private void Awake()
    {
        apiUrl = apiUrls[selectApiUrlNum];
    }

    // Unity UI 컴포넌트 참조
    public TMP_InputField signUpIDInputField;
    public TMP_InputField signUpPWInputField;
    public TMP_Text signUpResultText; // API 요청 결과를 출력

    // SignUp 요청을 처리
    public void SignUp()
    {
        string loginID = signUpIDInputField.text;
        string password = signUpPWInputField.text;
        StartCoroutine(SignUpRequest(loginID, password));
    }

    // SignUpRequest 코루틴
    IEnumerator SignUpRequest(string SignUpID, string SignUpPassword)
    {
        // apiUrl의 끝에 있는 슬래시를 제거하고, "/signup/"을 추가
        string signUpEndpoint = apiUrl.TrimEnd('/') + "/signup/";

        // JSON 형식의 데이터를 생성
        var requestData = new SignUpRequestData
        {
            Login_ID = SignUpID,
            Login_Password = SignUpPassword
        };
        string jsonData = JsonUtility.ToJson(requestData);

        Debug.Log(signUpEndpoint);

        using (UnityWebRequest request = new UnityWebRequest(signUpEndpoint, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error: " + request.error);
                signUpResultText.text = "SignUp Failed: " + request.error;
            }
            else
            {
                Debug.Log("SignUp Success: " + request.downloadHandler.text);
                signUpResultText.text = "SignUp Success!";
            }
        }
    }

    public TMP_InputField loginIDInputField;
    public TMP_InputField loginPasswordInputField;
    public TMP_Text loginResultText;

    public void Login()
    {
        StartCoroutine(LoginRequest(loginIDInputField.text, loginPasswordInputField.text));
    }

    IEnumerator LoginRequest(string loginID, string loginPassword)
    {
        string loginEndpoint = apiUrl.TrimEnd('/') + "/login/";
        var requestData = new LoginRequestData
        {
            Login_ID = loginID,
            Login_Password = loginPassword
        };
        string jsonData = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(loginEndpoint, jsonData))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error: " + request.error);
                loginResultText.text = "Login Failed: " + request.error;
            }
            else
            {
                Debug.Log("Login Success: " + request.downloadHandler.text);
                loginResultText.text = "Login Success!";

                // 이후 서버로부터 받은 사용자 데이터 처리(현재 씬 상태라면 일단, 유저 정보 보여주는 곳에 업데이트)
            }
        }
    }

}