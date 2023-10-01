using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCollapse : MonoBehaviour
{
    public bool speedUncapped;
    [SerializeField] float effectiveSpd, percentSpd, minSpd;
    Transform trfm;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        Destroy(GetComponent<SpriteRenderer>());
    }

    Vector3 vect3 = Vector3.zero;
    void FixedUpdate()
    {
        if (!speedUncapped)
        {
            vect3.x = minSpd;
            vect3.y = GameManager.cameraTrfm.position.y + 15 - trfm.position.y;
            trfm.position += vect3;
        }
        else
        {
            vect3.x = (GameManager.playerTrfm.position.x - trfm.position.x) * percentSpd;
            if (Mathf.Abs(vect3.x) < minSpd)
            {
                if (vect3.x > 0) { vect3.x = minSpd; }
                else { vect3.x = -minSpd; }
            }
            vect3.y = GameManager.playerTrfm.position.y + 15 - trfm.position.y;

            effectiveSpd = vect3.x;

            trfm.position += vect3;
        }
    }
}
