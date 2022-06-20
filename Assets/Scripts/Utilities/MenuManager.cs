using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        StartCoroutine(nameof(LoadYourAsyncScene));
    }

    private IEnumerator LoadYourAsyncScene()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("Scenes/MainScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
