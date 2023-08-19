using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    [SerializeField] ObjectPooler m_SlashFXPooler;
    public static ObjectPooler BloodFXPooler;
    public static ObjectPooler SlashFXPooler;

    private void Awake()
    {
        BloodFXPooler = m_BloodFXPooler;
        SlashFXPooler = m_SlashFXPooler;
    }
}
