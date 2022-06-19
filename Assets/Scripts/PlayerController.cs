using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Config")]
    public PlayerData playerData;
    public float MoveSpeed;
    public Vector2 Movement;
    public Vector2 LastMovement;
    public Vector2 LastPosition;
    public Vector2 MoveTargetPos;
    public bool flipx { get; private set; }
    

    [Header("Skill")]
    public Transform SkillParant;
    [Tooltip("Skill you can learn")]
    public List<SkillData> levelupskillpool = new List<SkillData>();
    [Tooltip("CurrentSkill")]
    public List<Skill> BaseSkills;

    [Header("Param")]
    public int level;
    public int exp;
    public int upgradeExpRequire;
    public int baseExpRequire = 1;
    public int increaseExpRequire = 2;
    public int hp;
    public int lucky;

    [Header("VFX")]
    public SpriteAfterImagePool AfterImagePool;
    public float Afterimageinterval = 0.1f;

    private PlayerAction _playerAction;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float afterimagecurrent;

    public EnemyController NearestEnemy { get => _nearestenemy; }
    private EnemyController _nearestenemy;
    private float _nearestdist;

    private void Awake()
    {
        _playerAction = new PlayerAction();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        var skills = SkillParant.GetComponentsInChildren<Skill>();
        BaseSkills = new List<Skill>(skills);

        afterimagecurrent = 0f;
    }

    private void OnEnable()
    {
        _playerAction.Enable();
    }

    private void OnDisable()
    {
        _playerAction.Disable();
    }

    private void Update()
    {
        var direction = _playerAction.Player_Map.Movememet.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            foreach (var e in BaseSkills)
            {
                e.Direction = direction;
            }
            //_animator.enabled = true;

            afterimagecurrent += Time.deltaTime;
            if (afterimagecurrent > Afterimageinterval)
            {
                afterimagecurrent = 0;
                AfterImagePool.Create(_spriteRenderer);
            }
        }
        else
        {
            //_animator.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Movement = _playerAction.Player_Map.Movememet.ReadValue<Vector2>();

        _rigidBody.velocity = Movement * MoveSpeed;
        _animator.SetFloat("Speed", Movement.magnitude * MoveSpeed);

        if (Movement != Vector2.zero)
        {
            if (Movement.x < 0)
            {
                _spriteRenderer.flipX = flipx = true;
            }
            else if (Movement.x > 0)
            {
                _spriteRenderer.flipX = flipx = false;
            }
            
            LastMovement = Movement;
            LastPosition = transform.position;
        }

    }



    public void ResetPlayer()
    {
        level = 1;
        exp = 0;
        RefillHP();
        UpdateUpgradeExpRequirement();
        GameManager.OnLevelUpdate.Invoke(level);
        GameManager.OnExpUpdate.Invoke(exp, upgradeExpRequire);
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        if (this.exp >= upgradeExpRequire)
        {
            this.exp = 0;
            level += 1;
            UpdateUpgradeExpRequirement();
            GameManager.OnLevelUpdate.Invoke(level);
            RefillHP();
        }
        GameManager.OnExpUpdate.Invoke(this.exp, upgradeExpRequire);
    }

    private void UpdateUpgradeExpRequirement()
    {
        upgradeExpRequire = 0;
        for (int i = 0; i < level + 1; i++)
        {
            upgradeExpRequire += baseExpRequire + increaseExpRequire * i;
        }
    }

    public void RefillHP()
    {
        hp = 100;
        GameManager.OnHPUpdate.Invoke(hp);
    }

    public void GetDamage(int damage)
    {
        ReduceHP(damage);
    }

    public void ReduceHP(int point)
    {
        hp -= point;
        hp = Mathf.Max(0, hp);
        GameManager.OnHPUpdate.Invoke(hp);
        if (hp == 0)
        {
            GameManager.OnDeath.Invoke();
            GameManager.Instance.StopGame();
        }
    }

    public int GetSkillLevel(SkillType type)
    {
        var skill = BaseSkills.Find(t => { return t.GetSkillType() == type; });
        return skill ? skill.Level : 0;
    }

    public void UpgradeSkill(SkillType type)
    {
        var skill = BaseSkills.Find(t => { return t.GetSkillType() == type; });

        if (skill)
        {
            skill.Levelup();
        }
        else
        {
            Debug.LogError("CANt Find Skill of type " + type);
        }
    }

    public void Attack()
    {
        _animator.Play("Attack");
    }

    public void UpdateNearstEnemy( EnemyController enemy)
    {
        if (!enemy) return;
        if(!_nearestenemy)
        {
            _nearestenemy = enemy;
            _nearestdist = Vector2.Distance(enemy.transform.position, transform.position);
        }
        else
        {
            var dist = Vector2.Distance(enemy.transform.position, transform.position);
            if(dist< _nearestdist)
            {
                _nearestenemy = enemy;
                _nearestdist = dist;
            }
        }
    }

}
