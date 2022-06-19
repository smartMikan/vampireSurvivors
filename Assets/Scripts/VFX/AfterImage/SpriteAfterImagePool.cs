using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAfterImagePool : MonoBehaviour
{
    public ObjectPool ObjectPool;
    public GameObject prefab;

    public float Duration;
    public float Fadeouttime;
    public Color StartColor;

    private void Awake()
    {
       ObjectPool = new ObjectPool(prefab, transform);
    }

    
    public GameObject Create(SpriteRenderer spriteRenderer)
    {
        var trans = spriteRenderer.transform;
        var pos = trans.position;
        var dir = trans.right;
        var scale = trans.lossyScale;
        var sp = spriteRenderer.sprite;
        var flipx = spriteRenderer.flipX;
        var flipy = spriteRenderer.flipY;
        var obj = ObjectPool.Instantiate(pos).GetComponent<SpriteAfterImageObject>();
        obj.Init(dir, scale, Duration, sp, StartColor, Fadeouttime, flipx, flipy);
        return obj.gameObject;
    }

}
