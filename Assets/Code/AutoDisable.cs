using UnityEngine;
using System.Collections;

public class AutoDisable : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine("DisableBlood");
    }

    IEnumerator DisableBlood()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
