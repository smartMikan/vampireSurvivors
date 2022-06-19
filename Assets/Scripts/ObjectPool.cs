using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPool : IObjectPool
{
    public GameObject Prefab;
    public Transform Parant;
    public List<GameObject> Objects;

    public ObjectPool(GameObject prefab = null, Transform parant = null)
    {
        Prefab = prefab;
        Parant = parant;
        Objects = new List<GameObject>();
    }

    private GameObject Create(GameObject gameObject, Vector3 position, Transform parant)
    {
        var obj = Object.Instantiate(gameObject, parant);
        obj.transform.position = position;
        Objects.Add(obj);
        return obj;
    }

    public GameObject Instantiate(Vector3 position)
    {
        if (Objects.Count > 0)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (!Objects[i].activeInHierarchy)
                {
                    Objects[i].transform.position = position;
                    Objects[i].SetActive(true);
                    return Objects[i];
                }
            }
        }

        return Create(Prefab, position, Parant);
    }

    public void Destroy(GameObject obj)
    {
        if (Objects.Contains(obj))
        {
            obj.SetActive(false);
        }
    }
}
