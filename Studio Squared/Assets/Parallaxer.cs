using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
    [SerializeField] Transform[] layers;
    [SerializeField] float xFactor, yFactor;

    Vector2[] calculatedRates;
    public Vector2 referencePoint;

    Vector2 vect2;

    void Start()
    {
        transform.parent = GameManager.cameraTrfm;

        calculatedRates = new Vector2[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            calculatedRates[i].x = 1 - 1 / Mathf.Pow(layers[i].position.z / 10, 2) * xFactor;
            calculatedRates[i].y = 1 - 1 / Mathf.Pow(layers[i].position.z / 10, 2) * yFactor;
        }
    }

    void SetReferencePoint(Vector2 position)
    {
        referencePoint = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            vect2.x = (GameManager.cameraTrfm.position.x - referencePoint.x) * calculatedRates[i].x;
            vect2.y = (GameManager.cameraTrfm.position.y - referencePoint.y) * calculatedRates[i].y;

            //vect2.x = (cameraTrfm.position.x - referencePoint.x) * xFactor;
            //vect2.y = (cameraTrfm.position.y - referencePoint.y) * yFactor;

            layers[i].position = vect2;
        }
    }
}