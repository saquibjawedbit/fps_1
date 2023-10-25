using UnityEngine;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public string[] sentence1;
    public string[] sentence2;
    public string[] sentence3;

    public void SetText(int sentnceIndex = 1)
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        int languageIndex = PlayerPrefs.GetInt("LANGUAGE");
        if (sentnceIndex == 1) text.text = sentence1[languageIndex];
        else if (sentnceIndex == 2) text.text = sentence2[languageIndex];
        else text.text = sentence3[languageIndex];
    }




}
