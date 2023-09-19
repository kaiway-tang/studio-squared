using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deparent : MonoBehaviour
{
    [SerializeField] Transform[] children;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].parent = null;
        }
        Destroy(this);
    }
}
