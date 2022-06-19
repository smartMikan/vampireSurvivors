using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelupUI : MonoBehaviour
{
    public Image SkillIcon;
    public TMP_Text SkillText;

    private string skillstring = "{0} Lv.{1}";


    public void SetSkill( SkillData data, int lv = 1)
    {
        SkillIcon.sprite = data.icon;
        SkillText.SetText(string.Format(skillstring, data.skillname, lv));
    }
}
