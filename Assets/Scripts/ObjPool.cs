using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[System.Serializable]
public class ObjPool
{
    private static ObjPool m_instance;

    public static ObjPool M_instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new ObjPool();
            }

            return m_instance;
        }
    }

    private ObjPool()
    {
        m_instance = this;
    }
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private int count;
    private List<GameObject> pool;

    public GameObject AddToPool
    {
        set
        {
            pool.Add(value);
            value.SetActive(false);
        }
    }

    public GameObject GetObject
    {
        get
        {
            if (pool.Count == 0)
            {
                return null;
            }

            var rand = UnityEngine.Random.Range(0, pool.Count);
            if (pool[rand].activeSelf == false)
            {
                return pool[rand];
            }

            return null;
        }
    }

    public void Init()
    {
        pool = new List<GameObject>();
        
        float rand = Random.value;

        for (int i = 0; i < count; i++)
        {
            AddToPool = GameObject.Instantiate(prefab[i]);
        }
        
    }
}