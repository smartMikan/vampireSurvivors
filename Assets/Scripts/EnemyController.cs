using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> VisibleEnemies = new List<EnemyController>();

    public ExpController exp;

    public int HP = 1000;
    public float Speed = .01f;
    public int damage = 10;
    public float atkinterval = 0.5f;

    public bool flipinv = false;

    public List<DamageBuff> Buff = new List<DamageBuff>();

    private PlayerController _player;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private float _attackcooldown = 0.0f;

    private void Start()
    {
        _player = GameManager.Instance.myPlayer;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.material = Instantiate(_sr.material);

        HP += GameManager.Instance.time;

        _attackcooldown = 0.0f;
    }

    private void Update()
    {
        var current = _sr.material.GetFloat("_HitEffectBlend");
        current -= .05f;
        current = Mathf.Max(current, 0);
        _sr.material.SetFloat("_HitEffectBlend", current);

        for (int i = 0; i < Buff.Count; i++)
        {
            Buff[i].CurrentInterval += Time.deltaTime;
            Buff[i].CurrentDuration += Time.deltaTime;
            if (Buff[i].CurrentInterval >= Buff[i].Interval)
            {
                Buff[i].CurrentInterval = 0;
                GetDamage(Mathf.Max(Buff[i].Damage, 0));
            }
            if (Buff[i].CurrentDuration >= Buff[i].Duration)
            {
                Buff[i].MarkRemove = true;
            }
        }
        Buff.RemoveAll(x => x.MarkRemove);

        
    }

    private void FixedUpdate()
    {
        var direction = (_player.transform.position - transform.position).normalized;
        _rb.velocity = direction * Speed;
        _sr.flipX = flipinv ? (_rb.velocity.x > 0) : (_rb.velocity.x < 0);

        _player.UpdateNearstEnemy(this);
    }

    public void GiveDamage(int dmg)
    {
        GetDamage(dmg);
    }

    public void Knockback(float kbdistance = 0.3f)
    {
        var direction = (transform.position - _player.transform.position).normalized * kbdistance;
        _rb.MovePosition(transform.position + direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var damage = collision.gameObject.GetComponent<Projectile>().Damage;
            GetDamage(damage);
            //Debug.LogWarning("Dammage!" + damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _attackcooldown -= Time.fixedDeltaTime;
            if (_attackcooldown <= 0)
            {
                _attackcooldown = atkinterval;
                var player = collision.gameObject.GetComponent<PlayerController>();
                player.GetDamage(damage);
            }
        }
    }

    private void GetDamage(int damage)
    {
        _sr.material.SetFloat("_HitEffectBlend", 1);
        HP -= damage;
        _2DCanvas.Instance.PopTextAt(damage.ToString(), transform.position);


        if (HP <= 0)
        {
            var obj = Instantiate(exp);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        VisibleEnemies.Add(this);
    }

    private void OnBecameInvisible()
    {
        VisibleEnemies.Remove(this);
    }
}
