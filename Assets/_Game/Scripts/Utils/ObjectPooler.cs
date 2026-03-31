using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i <= pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Lỗi: Không tìm thấy Tag [{tag}] trong Pooler!");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        poolDictionary[tag].Enqueue(objectToSpawn);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position =position;
        objectToSpawn.transform.rotation=rotation;
        IPoolable poolable = objectToSpawn.GetComponent<IPoolable>();
        if(poolable!=null) poolable.OnSpawn();
        return objectToSpawn;

    }


}
