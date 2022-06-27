using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] TMP_Text text;
    [SerializeField] float currentTime;
    [SerializeField] UnityEvent OnTimerEnd;
    private bool _isTimerActive = false;

    private void Awake()
    {
        currentTime++;
    }

    void Update()
    {
        if (_isTimerActive)
        {
            UpdateTime();
        }
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
        SceneManager.LoadScene("Scenes/Start Screen");
    }

    public void EnableTimer()
    {
        _isTimerActive = true;
    }
}