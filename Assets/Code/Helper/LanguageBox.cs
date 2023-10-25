using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class LanguageBox : MonoBehaviour
{

    public string[] text;

    private void Start() 
    {
        int index = PlayerPrefs.GetInt("LANGUAGE", 0);
        Text textBox = GetComponent<Text>();
        if (index == 3) textBox.fontSize = 45;
        textBox.text = text[index];
        Destroy(this, 0.5f);
    }
}
