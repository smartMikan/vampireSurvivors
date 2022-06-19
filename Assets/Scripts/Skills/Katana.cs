using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Skill
{
    public float Radius = 1.0f;
    public int amount = 1;
    public float interval = 0.1f;
    public float cooldown = 2.0f;

    public int lvlupdamage = 10;

    public GameObject SwingPrefab;
    private readonly List<GameObject> _swings = new List<GameObject>();
    private float _cooldown;
    private int _damage = 100;

    private void Start()
    {
        _cooldown = cooldown;
        amount = Level;
        Radius = Level;
        _damage = basedamage + Level * lvlupdamage;
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


    IEnumerator StartShoot()
    {
        for (int i = 0; i < amount; i++)
        {
            if (_swings.Count < amount)
            {
                var o = Instantiate(SwingPrefab, transform);
                _swings.Add(o);
            }
            var bullet = _swings[i];
            float flipxposition = Player.flipx ? 1.0f : -1.0f;
            float flipxamount = (i & 1) > 0 ? -1.0f : 1.0f;
            var swing = bullet.GetComponent<Swing>();
            swing.Damage = _damage;
            swing.radius = Radius;
            swing.offsetY = 0.2f * i;
            if (flipxamount * flipxposition > 0)
            {
                swing.flip = true;
            }
            else
            {
                swing.flip = false;
            }
            bullet.SetActive(true);

            yield return new WaitForSeconds(interval);
        }

    }

    public override SkillType GetSkillType()
    {
        return SkillType.katana;
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Level;
        _damage = basedamage + Level * lvlupdamage;
        Radius = Level;
    }
}
