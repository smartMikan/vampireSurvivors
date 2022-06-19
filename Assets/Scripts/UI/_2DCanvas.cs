using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _2DCanvas : MonoBehaviour
{
    public static _2DCanvas Instance;

    public GameObject _txt;

    public ObjectPool ObjectPool;
    public Transform ObjectPoolParant;

    private void Awake()
    {
        if (!Instance) {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

        ObjectPool = new ObjectPool(_txt, ObjectPoolParant);
    }

    public void PopTextAt(string text, Vector3 pos)
    {
        //Vector2 screenpos = Camera.main.WorldToScreenPoint(pos);
        var go = ObjectPool.Instantiate(pos).GetComponent<TMPro.TMP_Text>();
        go.text = text;
    }

}
