using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] public GameObject _panel;
    private bool _paused;

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void Pause()
    {
        if (_paused)
            return;

        _panel.SetActive(true);
        _paused = true;
        Time.timeScale = 0;
    }

    public void Play()
    {
        if (_paused == false)
            return;

        _panel.SetActive(false);
        _paused = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}