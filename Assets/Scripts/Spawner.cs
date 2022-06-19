using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int VisibleLimit = 50;
    public float Interval = .05f;
    public float Radius = 8;
    public bool HoriStage = false;
    public GameObject Enemy;

    public RectTransform screenrect;
    private Vector2 viewBound;

    private float _interval;

    private void Start()
    {
        viewBound = screenrect.localScale * screenrect.rect.size * 0.5f;
    }

    private void Update()
    {
        if (EnemyController.VisibleEnemies.Count > VisibleLimit)
        {
            _interval = Interval;
            return;
        }

        _interval += Time.deltaTime;
        if (_interval > Interval)
        {
            _interval = 0;
            SpawnRect();
        }
    }


    private void SpawnRect()
    {
        var center = transform.position;
        var left = center.x - viewBound.x;
        var right = center.x + viewBound.x;
        var up = center.y + viewBound.y;
        var down = center.y - viewBound.y;
        var hori = Random.Range(left, right) + Random.Range(-1f, 1f);
        var verti = Random.Range(up, down) + Random.Range(-1f, 1f);
        int i = Random.Range(0, HoriStage ? 2 : 4);
        var pos = Vector3.zero;
        switch (i)
        {
            case 0: // left
                pos.x = left;
                pos.y = verti;
                break;
            case 1: // right
                pos.x = right;
                pos.y = verti;
                break;
            case 2: // top
                pos.x = hori;
                pos.y = up;
                break;
            case 3: // down
                pos.x = hori;
                pos.y = down;
                break;
        }
        var obj = Instantiate(Enemy);
        obj.transform.position = pos;
    }

    private void SpawnCircle()
    {
        var center = transform.position;
        var angle = Random.Range(0, 360);
        var x = center.x + Radius * Mathf.Cos(angle * Mathf.PI / 180);
        var y = center.y + Radius * Mathf.Sin(angle * Mathf.PI / 180);
        var obj = Instantiate(Enemy);
        obj.transform.position = new Vector3(x, y, center.z);
    }
}
