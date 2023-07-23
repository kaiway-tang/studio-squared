using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] int life;
    public Transform trfm;
    int timer, objectID;

    protected void OnEnable()
    {
        timer = life;
    }

    public void Setup(ObjectPooler pObjectPooler, int pObjectID)
    {
        objectPooler = pObjectPooler;
        objectID = pObjectID;
        gameObject.SetActive(false);
    }

    protected void Destantiate()
    {
        if (objectID == 0)
        {
            Destroy(gameObject);
            return;
        }

        objectPooler.SetReady(objectID - 1);
        gameObject.SetActive(false);
    }
    protected void FixedUpdate()
    {
        if (timer > 0)
        {
            if (timer == 1)
            {
                timer = life;
                Destantiate();
            }
            timer--;
        }
    }
}