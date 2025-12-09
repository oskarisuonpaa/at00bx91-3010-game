using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance; // global access point
    public AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persist across scene changes
        }
        else
        {
            Destroy(gameObject); // prevent duplicates
        }
    }
    public void PlaySound(AudioClip clip)
    {
        if(clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
