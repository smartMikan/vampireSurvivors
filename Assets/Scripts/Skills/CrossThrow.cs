using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossThrow : MagicShoot
{
    public float Range = 1f;

    public SpriteAfterImagePool SpriteAfterImagePool;

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
            StartCoroutine(StartShoot());

        }
    }

    public override SkillType GetSkillType()
    {
        return SkillType.cross;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        //Range = Level * 0.1f + 1f;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = Mathf.Max(interval * amount, cooldown - Level * levelupcooldown);
    }


    IEnumerator StartShoot()
    {
        for (int i = 0; i < amount; i++)
        {
            while (!Player.NearestEnemy) yield return new WaitForFixedUpdate();
            var dir = (Player.NearestEnemy.transform.position - transform.position).normalized;
            Shoot(dir, dir);
            yield return new WaitForSeconds(interval);
        }

    }


    protected override void Shoot(Vector2 dir, Vector2 vdir)
    {
        var magicshootobj = ObjectPool.Instantiate(transform.position);
        var cross = magicshootobj.GetComponent<Cross>();
        cross.Init(this, _damage, _speed, dir, Range);
    }
}
