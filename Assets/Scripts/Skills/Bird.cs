using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Skill
{
    public ObjectPool ExplodePool; // Explode VFX Pool
    public Transform ExplodePoolParant;
    public int lvlupdamage = 10;
    public float basespeed = 0.6f;
    public float levelupspeed = 0.1f;
    public float angularspeed = 30f;
    public int amount = 5;
    public float interval = 0.1f;
    public float cooldown = 5.0f;
    public float levelupcooldown = 0.1f;
    public float Range = 1;

    protected int _damage = 100;
    protected float _speed = 0.6f;
    protected float _cooldown;

    public GameObject MissilePrefab;
    private Vector3 offsetOrigin = new Vector3(0.3f, 0.3f, 0);

    public GameObject Circle;
    public float CircleRange = 2f;
    public float CircleDistance = 3f;
    public float CircleSpeed = 180f;

    public SpriteRenderer BirdView;
    public GameObject ExplodeView;

    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
        ObjectPool = new ObjectPool(MissilePrefab, ObjectPoolParant);
        ExplodePool = new ObjectPool(ExplodeView, ExplodePoolParant);
    }

    public override SkillType GetSkillType()
    {
        throw new System.NotImplementedException(); // 
    }

    public override void Levelup()
    {
        Level += 1;
        amount = Mathf.Max(Level * 5, 1);
        Range = Mathf.Max(Mathf.Min(0.2f * Level, 1f), 0.2f);
        _speed = basespeed + Level * levelupspeed;
        _damage = basedamage + Level * lvlupdamage;
        interval = 1f / amount;
        _cooldown = Mathf.Max(interval * amount, cooldown - Level * levelupcooldown);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Player.transform.position + offsetOrigin;
        _damage = basedamage + lvlupdamage * Level;
        _speed = basespeed + Level * levelupspeed;
        _cooldown = cooldown;

        Circle.transform.localScale = new Vector3(CircleRange, CircleRange, 1);
        Circle.transform.position = Player.transform.position + Vector3.right * CircleDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Level == 0) return;
        _cooldown += Time.deltaTime;
        if (_cooldown > cooldown)
        {
            _cooldown = 0;
            StartCoroutine(StartShoot());
        }

    }

    private void FixedUpdate()
    {
        // bird follow player
        var pos = transform.position;
        var ppos = Player.transform.position;
        var off = ppos + new Vector3(offsetOrigin.x * (Player.flipx ? 1 : -1), offsetOrigin.y, 0);
        if (Vector3.Distance(pos, ppos) > 20f) transform.position = off;
        transform.position = Vector3.MoveTowards(pos, off, 2 * Time.fixedDeltaTime);
        var birddir = off - pos;
        if (birddir.x > 0) BirdView.flipX = true;
        else BirdView.flipX = false;
        // circle rotate
        var dir = Circle.transform.position - ppos;
        dir = Quaternion.Euler(0, 0, CircleSpeed * Time.fixedDeltaTime) * dir.normalized * CircleDistance;
        Circle.transform.position = ppos + dir;
    }

    protected virtual void Shoot(Vector3 target)
    {
        var missileobj = ObjectPool.Instantiate(transform.position);
        var missile = missileobj.GetComponent<HomingMissile>();
        missile.Init(ObjectPool, ExplodePool, _damage, Range, _speed, angularspeed, transform.position, target);
    }

    IEnumerator StartShoot()
    {
        var sp = Circle.GetComponent<SpriteRenderer>();
        var color = sp.color;
        color.a = 0.8f;
        sp.color = color;
        for (int i = 0; i < amount; i++)
        {
            Vector3 target = (Vector3)Random.insideUnitCircle * CircleRange + Circle.transform.position;
            Shoot(target);
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(interval);
        color.a = 0.2f;
        sp.color = color;
    }
}
