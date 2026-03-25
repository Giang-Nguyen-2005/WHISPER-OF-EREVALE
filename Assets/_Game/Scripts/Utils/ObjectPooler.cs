using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize=20;
    private List<GameObject> pooledObjects = new List<GameObject>();
    void Awake() {
    if (Instance == null) Instance = this;
    else Destroy(gameObject); // Nếu có cái thứ 2 thì xóa nó đi tránh có 2 pooled trong 1 scence
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj=Instantiate(bulletPrefab);//khởi tạo 1 gameobject = bulletprefab (object có sprite bullet)
            obj.SetActive(false);//set trạng thái không hiện thị là false
            pooledObjects.Add(obj);//thêm vào list ( list này gồm 20 viên đạn)
        }
    }
    public GameObject GetPooledObject()//hàm lấy đạn
    {
        foreach(GameObject obj in pooledObjects)// duyệt từng obj trong list
        {
            if(!obj.activeInHierarchy) return obj;// nếu ở trong hierarchy không active thì lấy obj đấy
        }
        // Tạo 1 viên đạn nếu trong list không có viên nào thỏa điều kiện
        GameObject newObj = Instantiate(bulletPrefab);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
}
