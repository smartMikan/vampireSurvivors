using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Skill
{
    public float Speed = 0.125f; // x*2pi each sec
    public int amount = 1;
    public float Radius = 3;
    public float baserange = .9f;
    public float lvluprange = 0.1f;
    public int lvlupdamage = 10;

    public FlowFireball Fireball;
    private readonly List<GameObject> _fireballs = new List<GameObject>();
    private int _damage = 100;
    private float _range = 1f;

    private void Start()
    {
        _damage = basedamage + Level * lvlupdamage;
        _range = baserange + Level * lvluprange;
        amount = Level;
        if(Level > 0)
        {
            CreateFireballs();
        }
    }

    public override SkillType GetSkillType()
    {
        return SkillType.guardian;
    }

    public override void Levelup()
    {
        Level += 1;
        _damage = basedamage + Level * lvlupdamage;
        _range = baserange + Level * lvluprange;
        amount = Level;

        CreateFireballs();
    }

    private void CreateFireballs()
    {
        float ind = FlowFireball.PI2 / amount;
        for (int i = 0; i < amount; i++)
        {
            if (_fireballs.Count < amount)
            {
                Fireball.transform.position = new Vector3(0, Radius);
                _fireballs.Add(Instantiate(Fireball, transform).gameObject);
            }
            var fb = _fireballs[i].GetComponent<FlowFireball>();
            fb.Damage = _damage;
            fb.range = _range;
            fb.speed = Speed;
            fb.radius = Radius;
            fb.rad = ind * i;
            fb.Init();
        }
    }

}
