using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private Text nValueText;
    [SerializeField] private Text sValueText;

    [SerializeField] private Slider nSlider;
    [SerializeField] private Slider sSlider;

    [Header("Volume")]
    [SerializeField] private Text vText;
    [SerializeField] private Slider vSlider;

    [Header("Graphic")]
    [SerializeField] private Dropdown graphicBox;
    [SerializeField] private Dropdown languageBox;

    private int currentlanguage = 0;


    // Start is called before the first frame update
    void Start()
    {
        int qualityIndex = PlayerPrefs.GetInt("QUALITY", 0);
        QualitySettings.SetQualityLevel(qualityIndex);
        nSlider.value = PlayerPrefs.GetInt("N_SENSETIVITY", 10);
        nValueText.text = nSlider.value.ToString();
        sSlider.value = PlayerPrefs.GetInt("S_SENSETIVITY", 70);
        sValueText.text = sSlider.value.ToString();
        graphicBox.value = PlayerPrefs.GetInt("QUALITY", 0);
        currentlanguage = PlayerPrefs.GetInt("LANGUAGE", 0);
        languageBox.value = currentlanguage;
    }

    public void SetTextValue(float value)
    {
        nValueText.text = value.ToString();
        PlayerPrefs.SetInt("N_SENSETIVITY", (int)value);
    }

    public void SetText2Value(float value)
    {
        sValueText.text = value.ToString();
        PlayerPrefs.SetInt("S_SENSETIVITY", (int)(value));
    }

    public void SetGraphic(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("QUALITY", index);
    }

    public void SetAudioPercentage(float value)
    {
        PlayerPrefs.SetFloat("VOLUME", value);
    }

    public void SetLanguage(int languageIndex)
    {
        if (currentlanguage == languageIndex) return;
        PlayerPrefs.SetInt("LANGUAGE", languageIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
