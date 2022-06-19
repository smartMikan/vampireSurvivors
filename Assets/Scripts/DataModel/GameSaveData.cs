using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameSaveData", menuName = "GameData/SaveData")]
public class GameSaveData : ScriptableObject
{
    public List<PlayerData> playerDatas;
    public List<SkillData> skillDatas;
}
