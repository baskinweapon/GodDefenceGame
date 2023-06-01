using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPool : MonoBehaviour {
    private List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private GameObject prefabs;
    
    void Awake() {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        // AsyncOperationHandle<GameObject> loadingOperation = objectToPool.LoadAssetAsync();
        // loadingOperation.Completed += OnLoadCompleted;
    }
    
    private bool isLoaded = false;
    private void OnLoadCompleted(AsyncOperationHandle<GameObject> obj) {
        isLoaded = true;
        prefabs = obj.Result;
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++) {
            tmp = Instantiate(obj.Result, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject() {
        for(int i = 0; i < amountToPool; i++) {
            if(!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        var tmp = Instantiate(objectToPool, transform);
        pooledObjects.Add(tmp);
        return tmp;
    }
}
