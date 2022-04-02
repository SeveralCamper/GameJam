using System.Collections.Generic;
using UnityEngine;

public class PoolMonobehaviour<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public T Prefab { get; }
    public Transform Container { get; private set; }

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
        var createdObject = Object.Instantiate(Prefab, Container);

        createdObject.gameObject.SetActive(defaultState);
        _pool.Add(createdObject);

        return createdObject;
    }

    private bool HasFreeObject(out T obj)
    {
        obj = null;

        foreach (var poolObject in _pool)
        {
            if (!poolObject.gameObject.activeInHierarchy)
            {
                obj = poolObject;
                obj.gameObject.SetActive(true);

                return true;
            }
        }

        return false;
    }

    public T GetObject()
    {
        if (HasFreeObject(out var poolObject)) { return poolObject; }
        else { return CreateObject(true); }
    }
}