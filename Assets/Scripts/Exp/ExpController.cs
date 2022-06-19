using UnityEngine;

public class ExpController : MonoBehaviour
{
    public int exp = 1;

    private float _moveSpeed = -2.5f;
    private ExpCollector _expCollector;
    private bool over = false;

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (!_expCollector || over) return;
        Vector2 dir = (_expCollector.transform.position - transform.position);
        transform.Translate(_moveSpeed * Time.fixedDeltaTime * dir.normalized);
        _moveSpeed += Time.fixedDeltaTime * 10.0f;
        if (dir.magnitude < .1f)
        {
            _expCollector.Player.AddExp(exp);
            Destroy(gameObject, 0.1f);
            over = true;
            return;
        }
    }

    public void SetCollector(ExpCollector collector)
    {
        _expCollector = collector;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep();
    }
}
