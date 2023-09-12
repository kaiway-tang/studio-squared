using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm, emptyTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    [SerializeField] ObjectPooler m_SlashFXPooler;
    [SerializeField] ObjectPooler m_LightningFXPooler;
    [SerializeField] ObjectPooler m_LightningPtclsPooler;
    [SerializeField] ObjectPooler m_TelegraphPooler;
    public static ObjectPooler BloodFXPooler;
    public static ObjectPooler SlashFXPooler;
    public static ObjectPooler LightningFXPooler;
    public static ObjectPooler LightningPtclsPooler;
    public static ObjectPooler TelegraphPooler;
    public static int terrainLayerMask;
    public const int PlayerCollisionLayer = 11;

    private void Awake()
    {
        terrainLayerMask = LayerMask.GetMask("Terrain");
        emptyTrfm = transform;

        BloodFXPooler = m_BloodFXPooler;
        SlashFXPooler = m_SlashFXPooler;
        LightningFXPooler = m_LightningFXPooler;
        LightningPtclsPooler = m_LightningPtclsPooler;
        TelegraphPooler = m_TelegraphPooler;
    }
}
