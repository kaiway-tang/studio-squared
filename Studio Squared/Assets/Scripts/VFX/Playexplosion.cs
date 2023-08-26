using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Playexplosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem[] ParticleSystems;
    [SerializeField] private int numExplosions;
    Vector3[] originalScale = new Vector3[3];
    
    void Awake()
    {
        ParticleSystems = GetComponentsInChildren<ParticleSystem>();
        int i = 0;
        foreach (ParticleSystem p in ParticleSystems)
        {
            originalScale[i] = p.transform.localScale;
            i++;
        }
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start the coroutine when the mouse button is pressed
            StartCoroutine(PlayExplosions());
        }
    }

    private IEnumerator PlayExplosions()
    {
        float xoffset = 0;
        float yoffset = 0;
        
    
        for (int i = 0; i < numExplosions; i++)
        {
            float scaleFactor = Random.Range(1f, 4f);
            
            int j = 0;
        
            foreach (ParticleSystem p in ParticleSystems)
            {
                if (j >= originalScale.Length)
                    break;
                xoffset += Random.Range(-2f, 2f);
                yoffset = Random.Range(-0.5f, 0.5f);
                p.transform.localScale = originalScale[j];
                Vector3 trsPos = p.transform.position;
                p.transform.position = new Vector3( xoffset,  yoffset, trsPos.z);
                p.transform.localScale = p.transform.localScale * scaleFactor;
                Debug.Log("{" + scaleFactor + "}," + "{" + p.transform.localScale + "}");
                p.Play();
                j++;
            }

            // Wait for 2 seconds before continuing the loop
            yield return new WaitForSeconds(0.05f);
        }
    }
    

}

