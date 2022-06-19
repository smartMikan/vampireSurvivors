using System;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public PlayerController Player;
    public Vector2 Direction = Vector2.right;
    public ObjectPool ObjectPool;
    public Transform ObjectPoolParant;
    public int Level;
    public int basedamage = 90;
    public abstract SkillType GetSkillType();
    public abstract void Levelup();

    private void Awake()
    {
        Player = transform.parent.GetComponentInParent<PlayerController>();
    }

}
