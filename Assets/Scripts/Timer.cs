using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] float currentTime;
    [SerializeField] UnityEvent OnTimerEnd;

    private void Awake()
    {
        currentTime *= 60;
    }

    void Update()
    {
        UpdateTime();
    }
    void UpdateTime()
    {
        currentTime = currentTime - Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        text.text = "Time: " + time.ToString(@"mm\:ss");
        if (currentTime <= 0)
        {
            OnTimerEnd.Invoke();
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}