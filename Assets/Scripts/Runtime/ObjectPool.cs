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

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool currentPool;

    private List<PooledObject> pool = new List<PooledObject>();

    public StringGameObjectDictionary objects;
    public Dictionary<string, GameObject> groups = new Dictionary<string, GameObject>();
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

    public void Add(string objName, string group)
    {
        GameObject groupObj;
        try
        {
            groupObj = groups[group];
        }
        catch (KeyNotFoundException)
        {
            groupObj = new GameObject();
            groupObj.transform.SetParent(objContainer);
            groups.Add(group, groupObj);
        }
        var obj = Instantiate(objects[objName], groupObj.transform);
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

    public GameObject Spawn(string objName, string group)
    {
        var pooledObject = pool.Find(x => x.name == objName);
        GameObject groupObj;
        try
        {
            groupObj = groups[group];
        }
        catch (KeyNotFoundException)
        {
            groupObj = new GameObject();
            groupObj.transform.SetParent(objContainer);
            groups.Add(group, groupObj);
        }
        if (pooledObject != null)
        {
            pool.Remove(pooledObject);
            pooledObject.obj.transform.SetParent(groupObj.transform);
            pooledObject.obj.SetActive(true);
            return pooledObject.obj;
        }
        else
        { 
            return Instantiate(objects[objName], groupObj.transform);
        }
    }
}
