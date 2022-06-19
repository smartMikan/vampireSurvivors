using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData : ScriptableObject
{


}

[CreateAssetMenu(fileName = "CharaData", menuName = "CharaData/PlayerData")]
public class PlayerData : CharacterData
{
    // achievements of this chara

    public bool Unlocked { get => unlocked; set => unlocked = value; }
    private bool unlocked;
}

