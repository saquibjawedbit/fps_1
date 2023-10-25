using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{

    [SerializeField] private GameObject batsEffect;
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private Slider loadingSlider;

    public void ChangeScene(int sceneIndex)
    {
        Time.timeScale = 1;
        if(batsEffect != null) batsEffect.SetActive(false);

        StartCoroutine(LoadSceneAsynchronuosly(sceneIndex));
    }

    IEnumerator LoadSceneAsynchronuosly(int sceneIndex)
    {
        loadingBar.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while(!asyncLoad.isDone)
        {
            loadingSlider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            yield return null;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Resume();
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Resume();
        ChangeScene(0);
    }

    public void pauseTheGame()
    {
        Time.timeScale = 0;
    }

    public void Next()
    {
        StartCoroutine(LoadSceneAsynchronuosly(SceneManager.GetActiveScene().buildIndex + 1));
    }
}
