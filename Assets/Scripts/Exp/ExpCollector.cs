using UnityEngine;

public class ExpCollector : MonoBehaviour
{
    public PlayerController Player;
    public float baseRadius = 0.4f;

    private const string _expTag = "EXP";
    private CircleCollider2D _collider;
    private int collectorLevel = 0;

    private void Start()
    {
        Player = GetComponentInParent<PlayerController>();
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = baseRadius + collectorLevel * .5f;
    }

    public void Levelup()
    {
        collectorLevel++;
        if (!_collider) return;
        _collider.radius = baseRadius + collectorLevel * .5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_expTag)) return;
        collision.GetComponent<ExpController>()?.SetCollector(this);
    }
}
