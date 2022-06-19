using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableSwordSkill : Skill
{
    public float cooldown;
    public GameObject swordPrefab;

    private float _currentTime;

    public override SkillType GetSkillType()
    {
        throw new System.NotImplementedException();
    }

    public override void Levelup()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        _currentTime = 0;
    }

    private void Update()
    {
        if (Level == 0) return;
        _currentTime += Time.deltaTime;
        if (_currentTime > Mathf.Max(cooldown, cooldown - Level * .1f))
        {
            Player.Attack();
            _currentTime = 0;
            swordPrefab.transform.position = transform.position;

            for (int i = 0; i < 360; i+=360 / Level)
            {
                var x = 10 * Mathf.Cos(i * Mathf.PI / 180);
                var y = 10 * Mathf.Sin(i * Mathf.PI / 180);
                var obj = Instantiate(swordPrefab);
                obj.transform.right = new Vector2(x, y);
            }
        }
    }
}
