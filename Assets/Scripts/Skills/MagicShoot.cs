using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShoot : Skill
{
    public int lvlupdamage = 10;
    public float basespeed = 0.6f;
    public float levelupspeed = 0.1f;
    public int amount = 1;
    public float interval = 0.1f;
    public float cooldown = 2.0f;
    public float levelupcooldown = 0.1f;

    public GameObject MagicShootPrefab;


    protected int _damage = 100;
    protected float _speed = 0.6f;
    protected float _cooldown;
    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
        ObjectPool = new ObjectPool(MagicShootPrefab, ObjectPoolParant);
    }

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
        return SkillType.magicshoot;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = Mathf.Max(interval * amount, cooldown - Level * levelupcooldown);
    }

    protected virtual void Shoot(Vector2 dir, Vector2 vdir)
    {
        var magicshootobj = ObjectPool.Instantiate(transform.position);
        var magicshoot = magicshootobj.GetComponent<MagicShootProj>();
        magicshoot.Init(this, _damage, _speed, dir, vdir);
    }

    IEnumerator StartShoot()
    {
        for (int i = 0; i < amount; i++)
        {
            while(!Player.NearestEnemy) yield return new WaitForFixedUpdate();
            var dir = (Player.NearestEnemy.transform.position - transform.position).normalized;
            Shoot(dir, dir);
            yield return new WaitForSeconds(interval);
        }

    }
}
