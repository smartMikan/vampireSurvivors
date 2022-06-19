using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighteousFire : Skill
{
    public int lvlupdamage = 10;

    public float Radius = 0.5f;
    public float Interval = 0.5f;
    public RighteousFireArea Area;

    private RighteousFireArea _area;
    private int _damage = 50;
    void Start()
    {
        _damage = basedamage + Level * lvlupdamage;
        if (Level > 0)
        {
            CreateDmgArea();
        }
    }

    public override SkillType GetSkillType()
    {
        return SkillType.righteousfire;
    }

    public override void Levelup()
    {
        Level += 1;
        if (!_area) CreateDmgArea();
        _damage = basedamage + Level * lvlupdamage;
        _area.Duration = _area.Interval = Interval;
        _area.Radius = Radius;
        _area.Damage = _damage;

    }

    private void CreateDmgArea()
    {
        Area.Duration = Area.Interval = Interval;
        Area.Radius = Radius;
        Area.Damage = _damage;
        _area = Instantiate(Area, transform);
    }
}
