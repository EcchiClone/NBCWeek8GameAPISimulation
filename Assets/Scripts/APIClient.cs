using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class APIClient : MonoBehaviour
{
    // API ��������Ʈ URL
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

    // Unity UI ������Ʈ ����
    public TMP_InputField loginIDInputField;
    public TMP_InputField passwordInputField;
    public TMP_Text resultText; // API ��û ����� ����� Text ������Ʈ

    // SignUp ��û�� ó���ϴ� �޼���
    public void SignUp()
    {
        string loginID = loginIDInputField.text;
        string password = passwordInputField.text;
        StartCoroutine(SignUpRequest(loginID, password));
    }

    // SignUpRequest �ڷ�ƾ
    IEnumerator SignUpRequest(string loginID, string password)
    {
        string signUpEndpoint = apiUrl + "/signup/";
        WWWForm form = new WWWForm();
        form.AddField("Login_ID", loginID);
        form.AddField("Login_Password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(signUpEndpoint, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError("Error: " + request.error);
                resultText.text = "SignUp Failed: " + request.error;
            }
            else
            {
                Debug.Log("SignUp Success: " + request.downloadHandler.text);
                resultText.text = "SignUp Success!";
            }
        }
    }

    // ��Ÿ API ��û �޼��� �߰� ����...
}
