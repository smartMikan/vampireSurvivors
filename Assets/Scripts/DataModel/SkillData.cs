using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName ="Skill/SkillData")]
public class SkillData : ScriptableObject
{
    [Header("UI")]
    public string skillname;
    public Sprite icon;
    public SkillType type;

    [Header("Object")]
    public GameObject prefab;

    public bool Unlocked { get => unlocked; set => unlocked = value; }
    private bool unlocked;
}
