using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStone : Skill
{
    public int lvlupdamage = 10;
    public float basespeed = 0.6f;
    public int amount = 1;
    public float range = 1f;
    public float duration = 5f;
    public float cooldown = 2.0f;
    public GameObject MagicStonePrefab;

    public RectTransform screenrect;
    public Rect Bound { get => _bound; private set => _bound = value; }
    private Rect _bound;
    private Vector2 viewBound;

    private int _damage = 100;
    private float _speed = 0.6f;
    private float _cooldown;

    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
        ObjectPool = new ObjectPool(MagicStonePrefab, ObjectPoolParant);
    }

    void Start()
    {
        if (!screenrect) screenrect = GameObject.FindGameObjectsWithTag("ScreenRectObj")[0].GetComponent<RectTransform>();
        viewBound = screenrect.localScale * screenrect.rect.size;

        _damage = basedamage + lvlupdamage * Level;
        _speed = basespeed;
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
            ShootStone();
        }

    }

    private void FixedUpdate()
    {
        var pos = transform.position;
        _bound.Set(pos.x - viewBound.x*0.5f, pos.y - viewBound.y*0.5f, viewBound.x, viewBound.y);
    }

    public override SkillType GetSkillType()
    {
        return SkillType.magicstone;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        _speed = basespeed;
    }

    private void ShootStone()
    {
        for (int i = 0; i < amount; i++)
        {
            var dir = Random.insideUnitCircle.normalized;
            Shoot(dir);
        }
    }

    private void Shoot( Vector2 dir)
    {
        var magicstoneobj = ObjectPool.Instantiate(transform.position);
        var magicstone = magicstoneobj.GetComponent<MagicStoneProj>();
        magicstone.Init(this, _damage, _speed, duration, range, dir);
    }
}
