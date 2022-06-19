using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MagicShoot
{
    public float Range = 1f;
    public int Passthrough = 1;

    public SpriteAfterImagePool SpriteAfterImagePool;

    void Start()
    {
        amount = Level;
        Passthrough = Level;
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

    public override SkillType GetSkillType()
    {
        return SkillType.tomahawk;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        Passthrough = Level;
        Range = Level * 0.1f + 1f;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = Mathf.Max(interval * amount, cooldown - Level * levelupcooldown);
    }



    private void ShootAll()
    {
        for (int i = 0; i < amount; i++)
        {
            var ldir = new Vector2(Random.Range(-_speed, _speed), Random.Range(6, 9));
            Shoot(ldir, ldir);
        }
    }


    protected override void Shoot(Vector2 dir, Vector2 vdir)
    {
        var magicshootobj = ObjectPool.Instantiate(transform.position);
        var axe = magicshootobj.GetComponent<Axe>();
        axe.Init(this, _damage, dir, vdir, Range, Passthrough);
    }
}
