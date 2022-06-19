using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : Skill
{
    
    public float Cooldown = 5f;
    public float Duration = 3;
    public float Range = 3;
    public float Amount = 1;
    public int lvlupdamage = 10;

    public Bottle Bottle;
    public BottleArea DamageArea;
    public RectTransform screenrect;

    private Vector2 viewBound;
    private float _cooldown;

    private int _damage = 100;

    private void Start()
    {
        viewBound = screenrect.localScale * screenrect.rect.size * 0.5f;
        _cooldown = 0f;
        Range = Level;
        _damage = basedamage + lvlupdamage * Level;
    }

    private void Update()
    {
        if (Level == 0) return;
        _cooldown += Time.deltaTime;
        if (_cooldown > Cooldown)
        {
            _cooldown = 0;
            ShootBottles();
        }
    }

    public override SkillType GetSkillType()
    {
        return SkillType.holywater;
    }

    public override void Levelup()
    {
        Level += 1;
        Range = Level;
        _damage = basedamage + lvlupdamage * Level;
    }


    private void ShootBottles()
    {
        var ipos = new Vector2(transform.position.x , transform.position.y);
        for (int i = 0; i < Amount; i++)
        {
            var dis = new Vector2(Random.Range(-viewBound.x, viewBound.x), Random.Range(-viewBound.y, viewBound.y));
            var target = ipos + dis;
            var pos = target + new Vector2(Random.Range(-2, 2), Random.Range(5, 8));
            Shoot(pos, target);
        }
    }

    private void Shoot(Vector3 pos, Vector3 target)
    {
        var obj = Instantiate(Bottle);
        obj.transform.position = pos;
        obj.Parent = this;
        obj.Target = target;
    }

    public void CreateArea(Vector3 pos)
    {
        var obj = Instantiate(DamageArea);
        obj.transform.position = pos;
        obj.Duration = Duration;
        obj.Radius = Range;
        obj.Damage = _damage;
    }
}
