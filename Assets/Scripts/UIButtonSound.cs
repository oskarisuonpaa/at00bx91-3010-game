using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clickClip;

    void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            if(UIAudioManager.Instance != null)
            {
                UIAudioManager.Instance.PlaySound(clickClip);
            }
        });
    }
}
