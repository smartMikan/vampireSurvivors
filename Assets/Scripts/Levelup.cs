using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Levelup : MonoBehaviour
{

    public List<Button> levelupbtns = new List<Button>();

    private PlayerController player;

    private void Awake()
    {
        player = GameManager.Instance.myPlayer;
    }

    private void OnEnable()
    {
        if (GameManager.Multiplay) return;
        GameManager.Instance.StopGame();

        var skill = DrawLevelupContents();
        if ((skill is null) || skill.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (i > skill.Count - 1) {
                levelupbtns[i].gameObject.SetActive(false);
            }
            else
            {
                CreateLevelupBtn(levelupbtns[i], skill[i]);
            }
                
        }

    }

    private void OnDisable()
    {
        if (GameManager.Multiplay) return;
        GameManager.Instance.ResumeGame();
    }

    public void Upgrade(SkillType type)
    {
        gameObject.SetActive(false);
        player.UpgradeSkill(type);
    }


    public List<SkillData> DrawLevelupContents()
    {
        var lucky = player.lucky;
        // todo: luck algo
        var levelupskillpool = player.levelupskillpool;
        int levelupcontentnum = Mathf.Min(4, levelupskillpool.Count);
        if(levelupcontentnum == 0)
        {
            return null;
        }

        return Utility.GetRandomElements(levelupskillpool, levelupcontentnum);
    }


    private void CreateLevelupBtn(Button button, SkillData data)
    {
        button.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Upgrade(data.type));
        var curlevel = player.GetSkillLevel(data.type);
        // TODO: "new" tag if curlevel == 0;

        button.GetComponent<LevelupUI>().SetSkill(data, curlevel + 1);
    }

}
