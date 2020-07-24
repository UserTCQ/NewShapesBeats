using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    public GameObject customBasePrefab;
    public GameObject animatedBasePrefab;
    public Transform customContainer;

    public StringGameObjectDictionary objects;
    public Dictionary<string, GameObject> groups = new Dictionary<string, GameObject>();
    public Transform objContainer;

    public void Start()
    {
        currentPool = this;
        if (Directory.Exists(Level.usingLevel.animatedObjsDir))
        {
            string[] subDirs = Directory.GetDirectories(Level.usingLevel.animatedObjsDir);
            foreach (var dir in subDirs)
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                FileInfo[] files = d.GetFiles("*.png").OrderBy(x => x.Name).ToArray();
                List<Sprite> frames = new List<Sprite>();
                foreach (var f in files)
                {
                    WWW w = new WWW("file://" + dir + "/" + f.Name);
                    frames.Add(Sprite.Create(w.texture, new Rect(0, 0, w.texture.width, w.texture.height), new Vector2(0.5f, 0.5f)));
                }
                var obj = Instantiate(animatedBasePrefab, customContainer);
                obj.SetActive(false);
                obj.GetComponent<AnimationRendererSynced>().frames = frames.ToArray();
                objects.Add(d.Name, obj);
            }
        }

        if (Directory.Exists(Level.usingLevel.customObjsDir))
        {
            DirectoryInfo d = new DirectoryInfo(Level.usingLevel.customObjsDir);
            FileInfo[] files = d.GetFiles("*.png");
            foreach (var f in files)
            {
                WWW w = new WWW("file://" + Level.usingLevel.customObjsDir + f.Name);
                var s = Sprite.Create(w.texture, new Rect(0, 0, w.texture.width, w.texture.height), new Vector2(0.5f, 0.5f));
                var obj = Instantiate(customBasePrefab, customContainer);
                obj.SetActive(false);
                obj.GetComponent<SpriteRenderer>().sprite = s;
                objects.Add(Path.GetFileNameWithoutExtension(f.Name), obj);
            }
        }
    }

    public void Add(string objName, int layer)
    {
        var obj = Instantiate(objects[objName], new Vector3(0, 0, layer), Quaternion.Euler(0, 0, 0), objContainer);
        obj.SetActive(false);
        pool.Add(new PooledObject(objName, obj));
    }

    public void Add(string objName, string group, int layer)
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
        var obj = Instantiate(objects[objName], new Vector3(0, 0, layer), Quaternion.Euler(0, 0, 0), groupObj.transform);
        obj.SetActive(false);
        pool.Add(new PooledObject(objName, obj));
    }

    public GameObject Spawn(string objName, int layer)
    {
        var pooledObject = pool.Find(x => x.name == objName);
        if (pooledObject != null)
        {
            pool.Remove(pooledObject);
            pooledObject.obj.SetActive(true);
            pooledObject.obj.transform.SetParent(objContainer);
            pooledObject.obj.transform.localPosition = new Vector3(0, 0, layer);
            return pooledObject.obj;
        }
        else
        {
            return Instantiate(objects[objName], new Vector3(0, 0, layer), Quaternion.Euler(0, 0, 0), objContainer);
        }
    }

    public GameObject Spawn(string objName, string group, int layer)
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
            pooledObject.obj.SetActive(true);
            pooledObject.obj.transform.SetParent(groupObj.transform);
            pooledObject.obj.transform.localPosition = new Vector3(0, 0, layer);
            return pooledObject.obj;
        }
        else
        { 
            return Instantiate(objects[objName], new Vector3(0, 0, layer), Quaternion.Euler(0, 0, 0), groupObj.transform);
        }
    }
}
