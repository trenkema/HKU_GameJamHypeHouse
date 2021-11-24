using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim;
    public string sceneName;
    public float waitTime = 1.5f;
    public float loadTime = 1.5f;
    public bool endScene = false;

    void Update()
    {
        if (endScene == true)
        {
            StartCoroutine(LoadScene());
            endScene = false;
        }
    }

    public void StartLoadScene()
    {
        StartCoroutine(LoadScene());
        endScene = false;
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(sceneName);
    }
}
