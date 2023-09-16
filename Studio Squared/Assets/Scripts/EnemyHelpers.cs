using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelpers : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] MobileEntity mobileEntity;
    public SpriteRenderer[] spriteRenderer;
    public static Material defaultMaterial, flashMaterial;

    private void Start()
    {
        if (!trfm) { trfm = transform; }
        if (!mobileEntity) { mobileEntity = GetComponent<MobileEntity>(); }
    }

    private void FixedUpdate()
    {
        if (hurtTimer > 0)
        {
            hurtTimer--;
            if (hurtTimer == 0)
            {
                for (int i = 0; i < spriteRenderer.Length; i++)
                {
                    spriteRenderer[i].material = defaultMaterial;
                }
            }
        }
    }

    public bool ObstructedSightLine(Vector2 start, Vector2 end)
    {
        return Physics2D.Linecast(start, end, GameManager.terrainLayerMask);
    }

    public bool PlayerInSight(Vector2 pos)
    {
        return !Physics2D.Linecast(pos, GameManager.playerTrfm.position, GameManager.terrainLayerMask);
    }

    public bool InBoxRangeToPlayer(float distance)
    {
        return Mathf.Abs(trfm.position.x - GameManager.playerTrfm.position.x) < distance && Mathf.Abs(trfm.position.y - GameManager.playerTrfm.position.y) < distance;
    }

    public Quaternion GetQuaternionToPlayerHead(Vector3 position)
    {
        GameManager.emptyTrfm.up = GameManager.playerTrfm.position - position;
        return GameManager.emptyTrfm.rotation;
    }
    public Quaternion GetQuaternionToPlayerPredicted(Vector2 position, float seconds)
    {
        GameManager.emptyTrfm.up = Player.GetPredictedPosition(seconds) - position;
        return GameManager.emptyTrfm.rotation;
    }

    public void FacePlayer()
    {
        if (GameManager.playerTrfm.position.x - trfm.position.x > 0) { mobileEntity.SetFacing(MobileEntity.RIGHT); }
        else { mobileEntity.SetFacing(MobileEntity.LEFT); }
    }

    public bool IsActive(float trackingRange, bool refreshCombat = true)
    {
        if (InBoxRangeToPlayer(trackingRange) && PlayerInSight(trfm.position))
        {
            if (refreshCombat) { CameraController.RefreshCombat(); }
            return true;
        }
        else
        {
            return false;
        }
    }

    int hurtTimer;

    public void FlashWhite()
    {
        for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].material = flashMaterial;
        }
        hurtTimer = 11;
    }
}
