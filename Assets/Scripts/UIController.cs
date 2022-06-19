using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Object")]
    public GameObject levelUp;

    [Header("Component")]
    public TMP_Text level;
    public TMP_Text timer;
    public Slider expBar;
    public TMP_Text message;
    public Slider hpBar;
    public GameObject deathScreen;
    public GameObject menu;

    [Header("String Format")]
    public string levelFormat = "Lv.{0}";
    public string timerFormat = "{0}:{1}";

    private Coroutine _hideMessageTask;

    private void Start()
    {
        GameManager.OnTimerUpdate.AddListener(SetTimer);
        GameManager.OnLevelUpdate.AddListener(SetLevel);
        GameManager.OnExpUpdate.AddListener(SetExp);
        //GameManager.OnExpUpdate.AddListener((x, y) => {
        //    SetMessage("HIC", .2f);
        //});
        GameManager.OnHPUpdate.AddListener(SetHP);
        GameManager.OnDeath.AddListener(() => deathScreen.SetActive(true));

        expBar.minValue = 0;
        message.enabled = false;

        levelUp.SetActive(false);
    }

    public void SetLevel(int level)
    {
        if (level == 1) return; // game start
        levelUp.SetActive(true);
        this.level.text = string.Format(levelFormat, level);
    }

    public void SetTimer(int minute, int second)
    {
        timer.text = string.Format(timerFormat, minute.ToString("00"), second.ToString("00"));
    }

    public void SetExp(int exp, int expMax)
    {
        expBar.maxValue = expMax;
        expBar.value = exp;
    }

    public void SetMessage(string text, float duration)
    {
        message.enabled = true;
        message.text = text;
        _hideMessageTask = StartCoroutine(HideMessage(duration));
    }

    private IEnumerator HideMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        message.enabled = false;
        _hideMessageTask = null;
    }

    public void SetHP(int hp)
    {
        hpBar.value = hp;
    }
}
