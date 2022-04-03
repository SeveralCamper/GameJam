using System.Collections.Generic;
using UnityEngine;

public class PoolMonobehaviour<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public T Prefab { get; }
    public Transform Container { get; private set; }
    public List<T> Pool => _pool;

    public PoolMonobehaviour(T prefab, Transform container, int count)
    {
        Prefab = prefab;
        Container = container;

        PoolCreate(count);
    }

    private void PoolCreate(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++) { CreateObject(); }
    }

    private T CreateObject(bool defaultState = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container.position, Quaternion.identity);

        createdObject.gameObject.SetActive(defaultState);
        _pool.Add(createdObject);

        return createdObject;
    }

    private bool HasFreeObject(out T obj, bool state)
    {
        obj = null;

        foreach (var poolObject in _pool)
        {
            if (!poolObject.gameObject.activeInHierarchy)
            {
                obj = poolObject;
                obj.gameObject.SetActive(state);

                return true;
            }
        }

        return false;
    }

    public T GetObject(bool state)
    {
        if (HasFreeObject(out var poolObject, state)) { return poolObject; }
        else { return CreateObject(state); }
    }

    public static PoolMonobehaviour<T> DestroyPool(ref PoolMonobehaviour<T> pool)
    {
        foreach (T obj in pool.Pool)
        {
            Object.Destroy(obj);
        }

        return pool;
    }
}