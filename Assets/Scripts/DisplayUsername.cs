using UnityEngine;
using TMPro;

public class DisplayUsername : MonoBehaviour
{
    public TMP_Text labelUsername;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!string.IsNullOrEmpty(UserSession.username))
        {
            labelUsername.text = "Welcome, " + UserSession.username + "!";
        }
        else
        {
            labelUsername.text = "Welcome!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}