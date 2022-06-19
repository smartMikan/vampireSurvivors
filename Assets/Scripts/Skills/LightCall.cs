using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCall : Skill
{
    public int lvlupdamage = 10;
    public int amount = 1;
    public float Range = 1f;
    public float cooldown = 6.0f;
    public float levelupcooldown = 0.1f;

    public GameObject ThunderPrefab;

    protected int _damage = 100;
    protected float _cooldown;

    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
        ObjectPool = new ObjectPool(ThunderPrefab, ObjectPoolParant);
    }

    void Start()
    {
        amount = Level;
        _damage = basedamage + lvlupdamage * Level;
        _cooldown = cooldown;
    }


    void Update()
    {
        if (Level == 0) return;
        _cooldown += Time.deltaTime;
        if (_cooldown >= cooldown)
        {
            //Player.Attack();
            _cooldown = 0;
            ShootAll();
        }
    }
    public override SkillType GetSkillType()
    {
        return SkillType.lightcall;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        _cooldown = cooldown - Level * levelupcooldown;
    }


    private void ShootAll()
    {
        var visibleenemies = EnemyController.VisibleEnemies;
        int targetnum = Mathf.Min(visibleenemies.Count, amount);
        var targets = visibleenemies.GetRandomElements(targetnum);
        foreach (var e in targets)
        {
            Shoot(e.transform.position);
        }
    }


    private void Shoot(Vector3 pos)
    {
        var thunder = ObjectPool.Instantiate(pos).GetComponent<Thunder>();
        thunder.Parent = this;
        thunder.Damage = _damage;
        thunder.transform.localScale = new Vector3(Range, Range, Range);
    }
}
