using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    #endregion

    #region NetWork
    public static bool Multiplay { get; private set; }
    #endregion

    #region Event
    public static UnityEvent OnDeath { get; set; } = new UnityEvent();
    public static UnityEvent<int, int> OnTimerUpdate { get; set; } = new UnityEvent<int, int>();
    public static UnityEvent<int> OnLevelUpdate { get; set; } = new UnityEvent<int>();
    public static UnityEvent<int> OnHPUpdate { get; set; } = new UnityEvent<int>();
    public static UnityEvent<int, int> OnExpUpdate { get; set; } = new UnityEvent<int, int>();
    #endregion

    #region Public
    [Header("Player")]
    public static PlayerController[] Players;
    public PlayerController myPlayer;

    [Header("Time")]
    public int time;

    [Header("UI")]
    public GameObject menu;

    [Header("Camera")]
    public GameObject maincamera;

    #endregion

    #region Private
    private Coroutine _timerTask;
    #endregion

    #region Message
    private void Awake()
    {
        if (Instance) DestroyImmediate(this);
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Invoke(nameof(StartGame), 1);
    }

    private void Update()
    {
        
    }
    #endregion

    public void StartGame()
    {
        // TODO: Multiplay = NetworkManager is not null;
        Time.timeScale = 1;
        StartTimer();
        myPlayer.ResetPlayer();
        //Levelup.guardianLevel = 1;
        //Levelup.bowLevel = 0;
        //Levelup.poisonLevel = 0;
        //Levelup.swordLevel = 0;

        SoundManager.Instance.PlayBGM();

        
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }

    #region Timer
    public void StartTimer()
    {
        ResetTimer();
        _timerTask = StartCoroutine(Timer());
    }

    private void ResetTimer()
    {
        time = 0;
        OnTimerUpdate.Invoke(0, 0);
        if (_timerTask != null) StopCoroutine(_timerTask);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            while(Time.timeScale == 0)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(1);
            time++;
            OnTimerUpdate.Invoke(time / 60, time % 60);
        }
    }
    #endregion

}
