using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm, emptyTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    [SerializeField] ObjectPooler m_SparkFXPooler;
    [SerializeField] ObjectPooler m_SlashFXPooler;
    [SerializeField] ObjectPooler m_LightningFXPooler;
    [SerializeField] ObjectPooler m_LightningPtclsPooler;
    [SerializeField] ObjectPooler m_TelegraphPooler;
    public static ObjectPooler BloodFXPooler;
    public static ObjectPooler SparkFXPooler;
    public static ObjectPooler SlashFXPooler;
    public static ObjectPooler LightningFXPooler;
    public static ObjectPooler LightningPtclsPooler;
    public static ObjectPooler TelegraphPooler;
    public static int terrainLayerMask;
    public const int PlayerCollisionLayer = 11, PlayerTriggerLayer = 1;

    [SerializeField] Material flashMaterial, defaultMaterial;

    public static int playerHP;

    private void Awake()
    {
        terrainLayerMask = LayerMask.GetMask("Terrain");
        emptyTrfm = transform;

        BloodFXPooler = m_BloodFXPooler;
        SparkFXPooler = m_SparkFXPooler;
        SlashFXPooler = m_SlashFXPooler;
        LightningFXPooler = m_LightningFXPooler;
        LightningPtclsPooler = m_LightningPtclsPooler;
        TelegraphPooler = m_TelegraphPooler;

        EnemyHelpers.flashMaterial = flashMaterial;
        EnemyHelpers.defaultMaterial = defaultMaterial;
    }

    public static void SaveSceneVariables()
    {
        playerHP = Player.self.HP;
    }

    public static void ResetGameVariables(bool resetAbilities = true)
    {
        playerHP = Player.self.maxHP;
        Player.mana = 0;

        if (resetAbilities)
        {
            Player.hasDoubleJump = false;
            Player.hasDash = false;
            Player.hasWallJump = false;
            Player.hasCast = false;
            Player.hasDashSlash = false;
        }
    }
}
