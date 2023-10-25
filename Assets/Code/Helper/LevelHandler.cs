using UnityEngine;

public class LevelHandler : MonoBehaviour
{

    [SerializeField] private Transform level_1;
    [SerializeField] private Transform level_2;

    private Vector3 pos;

    // Start is called before the first frame update
    void OnEnable()
    {

        UnityEngine.UI.Button[] b1 = level_1.GetComponentsInChildren<UnityEngine.UI.Button>();
        UnityEngine.UI.Button[] b2 = level_2.GetComponentsInChildren<UnityEngine.UI.Button>();

        int levelToUnlock = PlayerPrefs.GetInt("LEVEL", 0);

        for (int i = 1; i <= levelToUnlock; i++)
        {
            if (i <= 40) Unlock(b1[i]);
            else Unlock(b2[i]);
        }

        pos = level_1.GetComponent<RectTransform>().position;
    }

    void Unlock(UnityEngine.UI.Button button)
    {
        button.interactable = true;
        button.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
    }

    public void next()
    {
        level_2.GetComponent<RectTransform>().position = pos;
    }

    public void reverse()
    {
        level_2.GetComponent<RectTransform>().position = new Vector2(1000, 0);
    }
}
