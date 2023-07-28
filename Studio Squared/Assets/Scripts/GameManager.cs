using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    public static ObjectPooler BloodFXPooler;

    private void Awake()
    {
        BloodFXPooler = m_BloodFXPooler;
    }
}
