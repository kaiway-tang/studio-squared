using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    [SerializeField] ObjectPooler m_SlashFXPooler;
    [SerializeField] ObjectPooler m_LightningFXPooler;
    [SerializeField] ObjectPooler m_LightningPtclsPooler;
    public static ObjectPooler BloodFXPooler;
    public static ObjectPooler SlashFXPooler;
    public static ObjectPooler LightningFXPooler;
    public static ObjectPooler LightningPtclsPooler;
    public static int terrainLayerMask;

    private void Awake()
    {
        terrainLayerMask = LayerMask.GetMask("Terrain");
        BloodFXPooler = m_BloodFXPooler;
        SlashFXPooler = m_SlashFXPooler;
        LightningFXPooler = m_LightningFXPooler;
        LightningPtclsPooler = m_LightningPtclsPooler;
    }
}
