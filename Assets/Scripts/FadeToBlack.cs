using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public bool isComplete;
    private Image _fadeToBlack;
    // Start is called before the first frame update
    void Start()
    {
        _fadeToBlack = GetComponent<Image>();
        _fadeToBlack.color = Color.black;
        StartCoroutine(FadeOutFromBlack());
    }

    public void Fade()
    {
        StartCoroutine(FadeIntoBlack());
    }
    
    IEnumerator FadeOutFromBlack()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            _fadeToBlack.color = new Color(0, 0, 0, t);
            yield return null;
        }
        _fadeToBlack.color = new Color(0, 0, 0, 0);
    }
    
    IEnumerator FadeIntoBlack()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        isComplete = false;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            _fadeToBlack.color = new Color(0, 0, 0, t);
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
        isComplete = true;
        _fadeToBlack.color = new Color(0, 0, 0, 1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
