public enum SkillType
{
    invalid = 0,
    whip = 1,
    cross = 2,
    knife = 3,
    holywater = 4,
    bibe = 5,
    tomahawk = 6,
    magicstone = 7,
    agies = 8,
    righteousfire = 9,
    lightcall = 10,
    bone = 11,
    gun = 12,
    sword = 13,
    katana = 14,
    guardian = 15,
    magicshoot = 16,
    fireball = 17,
    whitebird = 18,
    blacebird = 19,
    rainbowbird = 20
}

public enum BuffType
{
    Invalid = 0,
    BottleDamage = 1,
    RighteousFireDamage = 2,
}

[System.Serializable]
public class DamageBuff
{
    public BuffType Type;
    public int Damage;
    public float Interval;
    public float CurrentInterval;
    public float Duration;
    public float CurrentDuration;
    public bool MarkRemove;
}