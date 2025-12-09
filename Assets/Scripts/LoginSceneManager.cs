using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Text;
using System;

public class LoginSceneManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("Login UI")]
    public TMP_InputField loginUsername;
    public TMP_InputField loginPassword;

    [Header("Register UI")]
    public TMP_InputField registerUsername;
    public TMP_InputField registerPassword;

    // === Play as Quest ===
    public void StartAsQuest()
    {
        SceneManager.LoadScene("StartScene");
    }

    // === Show Panels ===
    public void ShowLoginPanel()
    {
        mainMenuPanel.SetActive(false);
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void ShowRegisterPanel()
    {
        mainMenuPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    // === Login ===
    public void SendLogin()
    {
        string user = loginUsername.text;
        string pass = loginPassword.text;
        StartCoroutine(LoginRequest(user, pass));
    }

    private IEnumerator LoginRequest(string user, string pass)
    {
        string json = JsonUtility.ToJson(new Credentials(user, pass));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest("https://niisku.lab.fi/~suoos/agario/api/login.php", "POST");
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        Debug.Log("Response Code: " + req.responseCode);
        Debug.Log("Response Text: " + req.downloadHandler.text);

        if (req.result == UnityWebRequest.Result.Success)
        {
            string responseText = req.downloadHandler.text;

            // Guard: only parse if response looks like JSON
            if (!string.IsNullOrEmpty(responseText) && responseText.TrimStart().StartsWith("{"))
            {
                try
                {
                    LoginResponse res = JsonUtility.FromJson<LoginResponse>(responseText);
                    if (res.success)
                    {
                        UserSession.user_id = res.user_id;
                        UserSession.username = loginUsername.text;
                        Debug.Log("Login OK, user_id=" + res.user_id);
                        SceneManager.LoadScene("StartScene");
                    }
                    else
                    {
                        Debug.LogError("Login failed: " + res.error);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to parse login JSON: " + ex.Message);
                    Debug.LogError("Raw response: " + responseText);
                }
            }
            else
            {
                Debug.LogError("Invalid login response (not JSON): " + responseText);
            }
        }
        else
        {
            Debug.LogError("Login request error: " + req.error);
        }
    }

    // === Register ===
    public void SendRegister()
    {
        string user = registerUsername.text;
        string pass = registerPassword.text;
        StartCoroutine(RegisterRequest(user, pass));
    }

    private IEnumerator RegisterRequest(string user, string pass)
    {
        string json = JsonUtility.ToJson(new Credentials(user, pass));
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest req = new UnityWebRequest("https://niisku.lab.fi/~suoos/agario/api/register.php", "POST");
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        Debug.Log("Response Code: " + req.responseCode);
        Debug.Log("Response Text: " + req.downloadHandler.text);

        if (req.result == UnityWebRequest.Result.Success)
        {
            string responseText = req.downloadHandler.text;

            // Guard: only parse if response looks like JSON
            if (!string.IsNullOrEmpty(responseText) && responseText.TrimStart().StartsWith("{"))
            {
                try
                {
                    RegisterResponse res = JsonUtility.FromJson<RegisterResponse>(responseText);
                    if (res.success)
                    {
                        Debug.Log("Registration OK");
                        registerPanel.SetActive(false);
                        loginPanel.SetActive(true);
                    }
                    else
                    {
                        Debug.LogError("Register failed: " + res.error);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to parse register JSON: " + ex.Message);
                    Debug.LogError("Raw response: " + responseText);
                }
            }
            else
            {
                Debug.LogError("Invalid register response (not JSON): " + responseText);
            }
        }
        else
        {
            Debug.LogError("Register request error: " + req.error);
        }
    }
}

// === Helper classes for JSON serialization/deserialization ===
[Serializable]
public class Credentials
{
    public string username;
    public string password;

    public Credentials(string u, string p)
    {
        username = u;
        password = p;
    }
}

[Serializable]
public class RegisterResponse
{
    public bool success;
    public string error; // only if success == false
}

[Serializable]
public class LoginResponse
{
    public bool success;
    public int user_id;   // only if success == true
    public string error;  // only if success == false
}