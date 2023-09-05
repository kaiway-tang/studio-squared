using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize;
    PooledObject[] pooledObjects;
    GameObject[] gameObjects;
    bool[] objectReady;

    static Vector3 instantiationRotation;

    private void Start()
    {
        objectReady = new bool[poolSize];
        gameObjects = new GameObject[poolSize];
        pooledObjects = new PooledObject[poolSize];

        GameObject newObject;

        for (int i = 0; i < poolSize; i++)
        {
            newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            pooledObjects[i] = newObject.GetComponent<PooledObject>();
            pooledObjects[i].Setup(this, i + 1);

            gameObjects[i] = newObject;
            objectReady[i] = true;
        }
    }

    public Transform Instantiate(Vector2 position, float zRotation = 0)
    {
        instantiationRotation.z = zRotation;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (objectReady[i])
            {
                gameObjects[i].SetActive(true);
                pooledObjects[i].trfm.position = position;
                pooledObjects[i].trfm.localEulerAngles = instantiationRotation;
                objectReady[i] = false;

                return pooledObjects[i].trfm;
            }
        }

        return Instantiate(prefab, position, Quaternion.Euler(instantiationRotation)).transform;
        Debug.Log("not enough " + prefab);
    }

    public void SetReady(int pObjectID)
    {
        objectReady[pObjectID] = true;
    }
}