using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Transform playerTrfm, cameraTrfm, emptyTrfm;

    [SerializeField] ObjectPooler m_BloodFXPooler;
    [SerializeField] ObjectPooler m_BloodFXLargePooler;
    [SerializeField] ObjectPooler m_SparkFXPooler;
    [SerializeField] ObjectPooler m_SlashFXPooler;
    [SerializeField] ObjectPooler m_LightningFXPooler;
    [SerializeField] ObjectPooler m_LightningPtclsPooler;
    [SerializeField] ObjectPooler m_TelegraphPooler;
    public static ObjectPooler BloodFXPooler;
    public static ObjectPooler BloodFXLargePooler;
    public static ObjectPooler SparkFXPooler;
    public static ObjectPooler SlashFXPooler;
    public static ObjectPooler LightningFXPooler;
    public static ObjectPooler LightningPtclsPooler;
    public static ObjectPooler TelegraphPooler;
    public static int terrainLayerMask;
    public const int PlayerCollisionLayer = 11, PlayerTriggerLayer = 1;

    [SerializeField] Material flashMaterial, defaultMaterial;

    public static GameManager self;
    public static int playerHP;

    public static string targetScene;
    public static Vector2 spawnPosition;
    static bool firstStartComplete;

    private void Awake()
    {
        terrainLayerMask = LayerMask.GetMask("Terrain");
        emptyTrfm = transform;
        self = GetComponent<GameManager>();

        BloodFXPooler = m_BloodFXPooler;
        BloodFXLargePooler = m_BloodFXLargePooler;
        SparkFXPooler = m_SparkFXPooler;
        SlashFXPooler = m_SlashFXPooler;
        LightningFXPooler = m_LightningFXPooler;
        LightningPtclsPooler = m_LightningPtclsPooler;
        TelegraphPooler = m_TelegraphPooler;

        EnemyHelpers.flashMaterial = flashMaterial;
        EnemyHelpers.defaultMaterial = defaultMaterial;
    }

    private void Start()
    {
        if (!firstStartComplete)
        {
            SetPlayerSpawn("Mineshaft", new Vector2(12.1f, -12.072f));
            firstStartComplete = true;
        }
        else
        {
            playerTrfm.position = spawnPosition;
        }

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

    public static void LoadScene()
    {
        LoadScene(targetScene);
    }

    public static void LoadScene(string scene)
    {
        HUDManager.FadeBlackCoverOpacity(1);
        SaveSceneVariables();

        if (scene.Length > 0) { targetScene = scene; }
        else { targetScene = SceneManager.GetActiveScene().name; }

        Player.nextScene = "";
        self.Invoke("SetScene", 2);
    }

    public static void LoadScene(string scene, Vector2 position)
    {
        spawnPosition = position;
        LoadScene(scene);
    }

    public static void SetPlayerSpawn(string scene, Vector2 position)
    {
        if (scene.Length > 0) { targetScene = scene; }
        else { targetScene = SceneManager.GetActiveScene().name; }
        spawnPosition = position;
    }

    void SetScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
