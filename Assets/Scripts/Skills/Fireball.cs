using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MagicShoot
{

    void Start()
    {
        amount = Level;
        _damage = basedamage + lvlupdamage * Level;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = cooldown;
    }

    void Update()
    {
        if (Level == 0) return;
        _cooldown += Time.deltaTime;
        if (_cooldown >= cooldown)
        {
            //Player.Attack();
            _cooldown = 0f;
            ShootAll();

        }
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = Mathf.Max(interval * amount, cooldown - Level * levelupcooldown);
    }

    public override SkillType GetSkillType()
    {
        return SkillType.fireball;
    }

    private void ShootAll()
    {
        var dir = Random.insideUnitCircle.normalized;
        var enemylist = EnemyController.VisibleEnemies;
        var max = enemylist.Count -1;
        if(max > 0)
        {
            var n = Random.Range(0, max);
            var e = enemylist[n];
            dir = (e.transform.position - transform.position).normalized;
        }



        for (int i = 0; i < amount; i++)
        {
            var mul = (i & 1) == 1 ? -1 * ((i+1) >> 1) : (i >> 1);
            var ldir = Quaternion.Euler(0,0, mul * 10) * dir;
            Shoot(ldir, dir);
        }
    }
}
