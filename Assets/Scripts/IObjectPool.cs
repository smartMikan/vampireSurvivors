using UnityEngine;

public interface IObjectPool
{
    void Destroy(GameObject obj);
    GameObject Instantiate(Vector3 position);
}