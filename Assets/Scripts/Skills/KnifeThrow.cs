using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : Skill
{
    public int lvlupdamage = 10;
    public float basespeed = 0.6f;
    public int Passthrough = 1;
    public int amount = 1;
    public float interval = 0.1f;
    public float cooldown = 2.0f;
    public GameObject KnifePrefab;

    private int _damage = 100;
    private float _speed = 0.6f;
    private float _cooldown;

    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
        ObjectPool = new ObjectPool(KnifePrefab, ObjectPoolParant);
    }

    void Start()
    {
        _damage = basedamage + lvlupdamage * Level;
        _speed = basespeed;
        _cooldown = cooldown;
    }

    private void Update()
    {
        if (Level == 0) return;
        _cooldown += Time.deltaTime;

        if (_cooldown >= cooldown)
        {
            //Player.Attack();
            _cooldown = 0;
            StartCoroutine(StartShoot());
        }
    }


    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed;
        Passthrough = Level;
    }

    public override SkillType GetSkillType()
    {
        return SkillType.knife;
    }

    IEnumerator StartShoot()
    {
        for (int i = 0; i < amount; i++)
        {
            Shoot(Direction);
            yield return new WaitForSeconds(interval);
        }

    }

    private void Shoot(Vector2 dir)
    {
        var knifeobj = ObjectPool.Instantiate(transform.position);
        var knife = knifeobj.GetComponent<Knife>();
        knife.Init(this, dir, _damage, _speed, Passthrough);
    }
}
