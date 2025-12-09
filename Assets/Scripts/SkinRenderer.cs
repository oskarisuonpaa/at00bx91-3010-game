using UnityEngine;
using UnityEngine.UI;

public class SkinRenderer : MonoBehaviour
{
    
    public Image colorPreviewCircle;
    public Button[] colorButtons;
    public Color[] colorOptions;
    public Button confirmButton;
    private Color selectedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerColorR"))
        {
            float r = PlayerPrefs.GetFloat("PlayerColorR", selectedColor.r);
            float g = PlayerPrefs.GetFloat("PlayerColorG", selectedColor.g);
            float b = PlayerPrefs.GetFloat("PlayerColorB", selectedColor.b);
            float a = PlayerPrefs.GetFloat("PlayerColorA", selectedColor.a);
            selectedColor = new Color(r, g, b, a);
        }
        else
        {
            selectedColor = colorOptions[0];
        }
        for (int i = 0; i < colorButtons.Length; i++)
        {
            int index = i;
            colorButtons[i].onClick.AddListener(() => ChangeCircleColor(index));
        }
        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    void ChangeCircleColor(int index)
    {
        selectedColor = colorOptions[index];
        colorPreviewCircle.color = selectedColor;
    }

    void ConfirmSelection()
    {
        PlayerPrefs.SetFloat("PlayerColorR", selectedColor.r);
        PlayerPrefs.SetFloat("PlayerColorG", selectedColor.g);
        PlayerPrefs.SetFloat("PlayerColorB", selectedColor.b);
        PlayerPrefs.SetFloat("PlayerColorA", selectedColor.a);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
