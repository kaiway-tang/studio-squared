using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameManager.cameraTrfm;
        Destroy(GetComponent<AmbientParticles>());
    }
}
