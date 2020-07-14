using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject
{
    public string name;
    public GameObject obj;

    public PooledObject(string name, GameObject obj)
    {
        this.name = name;
        this.obj = obj;
    }
}

public class SpawnPool : MonoBehaviour
{
    public static SpawnPool currentPool;

    private List<PooledObject> pool = new List<PooledObject>();

    public StringGameObjectDictionary objects;
    public Transform objContainer;

    public void Start()
    {
        currentPool = this;
    }

    public void Add(string objName)
    {
        var obj = Instantiate(objects[objName], objContainer);
        obj.SetActive(false);
        pool.Add(new PooledObject(objName, obj));
    }

    public GameObject Spawn(string objName)
    {
        var pooledObject = pool.Find(x => x.name == objName);
        if (pooledObject != null)
        {
            pool.Remove(pooledObject);
            pooledObject.obj.SetActive(true);
            return pooledObject.obj;
        }
        else
        {
            return Instantiate(objects[objName], objContainer);
        }
    }
}
