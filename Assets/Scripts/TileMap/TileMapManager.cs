using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapManager : MonoBehaviour
{
    public List<Grid> Grids = new List<Grid>();
    public GameObject[,] grids;

    public Transform observetransform;



    private int scrollupr = 2;
    private int scrolldownr = 0;
    private int scrollrc = 0;
    private int scrolllc = 2;

    private Vector4 nexttransrect;

    private void Awake()
    {
        grids = new GameObject[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grids[i, j] = Grids[i*3 + j].gameObject;
            }
        }

        scrollupr = scrolllc = 2;
        scrolldownr = scrollrc = 0;

        nexttransrect = new Vector4(-23, -23, 24, 24);
    }

    private void Update()
    {
        if (!observetransform) return;

        if(observetransform.position.x > nexttransrect.z)
        {
            Scrollright();
        }

        if (observetransform.position.x < nexttransrect.x)
        {
            Scrollleft();
        }

        if (observetransform.position.y > nexttransrect.w)
        {
            Scrollup();
        }

        if (observetransform.position.y < nexttransrect.y)
        {
            Scrolldown();
        }
    }

    public void SetObervetarget(Transform trans)
    {
        observetransform = trans;
    }

    public void Scrollup()
    {
        for (int i = 0; i < 3; i++)
        {
            grids[scrollupr, i].transform.Translate(0,96,0);
        }
        scrollupr = (scrollupr + 3 - 1) % 3;
        scrolldownr = (scrolldownr + 3 - 1) % 3;

        nexttransrect.y += 32;
        nexttransrect.w += 32;
    }

    public void Scrolldown()
    {
        for (int i = 0; i < 3; i++)
        {
            grids[scrolldownr, i].transform.Translate(0, -96, 0);
        }
        scrollupr = (scrollupr + 1) % 3;
        scrolldownr = (scrolldownr + 1) % 3;

        nexttransrect.y -= 32;
        nexttransrect.w -= 32;
    }

    public void Scrollright()
    {
        for (int i = 0; i < 3; i++)
        {
            grids[i, scrollrc].transform.Translate(96, 0, 0);
        }
        scrollrc = (scrollrc + 1) % 3;
        scrolllc = (scrolllc + 1) % 3;

        nexttransrect.x += 32;
        nexttransrect.z += 32;
    }

    public void Scrollleft()
    {
        for (int i = 0; i < 3; i++)
        {
            grids[i, scrolllc].transform.Translate(-96, 0, 0);
        }
        scrollrc = (scrollrc + 3 - 1) % 3;
        scrolllc = (scrolllc + 3 - 1) % 3;

        nexttransrect.x -= 32;
        nexttransrect.z -= 32;
    }

}
