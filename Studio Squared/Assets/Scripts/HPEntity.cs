using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] public int maxHP, HP;
    [SerializeField] protected Transform trfm;
    [SerializeField] EntityType entityType;

    [SerializeField] int deathTraumaAmount = 15;

    [SerializeField] GameObject damagedFXObj;
    [SerializeField] ObjectPooler damagedFXPooler;

    public const int IGNORED = -1, ALIVE = 0, DEAD = 1;
    protected int ignoreAttackID;

    int invulnerable;

    public enum EntityType
    {
        Enemy, Player, PlayerPerfectDodge, Neutral
    }

    protected void Start()
    {
        if (HP == 0) { HP = maxHP; }
        if (!damagedFXObj && !damagedFXPooler) { damagedFXPooler = GameManager.BloodFXPooler; }
    }

    protected void FixedUpdate()
    {
        if (invulnerable > 0) { invulnerable--; }
    }

    public virtual int TakeDamage(int amount, Vector2 knockback, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        return TakeDamage(amount, entitySource, attackID);
    }

    public int TakeDamage(int amount, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        if (entitySource == entityType || !ValidAttackID(attackID) || invulnerable > 0) { return IGNORED; }

        HP -= amount;

        OnDamageTaken(amount, HP <= 0 ? DEAD : ALIVE);
        if (entityType == EntityType.PlayerPerfectDodge) { return IGNORED; }

        if (entitySource == EntityType.Player && HP > 0) { CameraController.EnterCombat(); }


        if (damagedFXObj)
        {
            Instantiate(damagedFXObj, trfm.position, Quaternion.identity);
        }
        else
        {
            damagedFXPooler.Instantiate(trfm.position, 0);
        }

        if (HP <= 0)
        {
            CameraController.SetTrauma(deathTraumaAmount);
            Destroy(trfm.gameObject);
            return DEAD;
        }
        return ALIVE;
    }

    protected virtual void OnDamageTaken(int amount, int result) { }
    protected virtual void OnHeal(int amount) { }

    protected int[] trackedAttackIDs = new int[3];
    protected int latestAttackIDIndex;
    bool ValidAttackID(int attackID) //returns False if attackID is found, True otherwise; also handles attackID tracking
    {
        bool result = !(trackedAttackIDs[0] == attackID || trackedAttackIDs[1] == attackID || trackedAttackIDs[2] == attackID);
        SetInvalidAttackID(attackID);
        return result || attackID == 0;
    }

    public void SetInvalidAttackID(int ID)
    {
        trackedAttackIDs[latestAttackIDIndex] = ID;
        latestAttackIDIndex++;
        if (latestAttackIDIndex > 2) { latestAttackIDIndex = 0; }
    }

    public void SetInvulnerable(int duration)
    {
        if (duration > invulnerable) { invulnerable = duration; }
    }

    public void Heal(int amount, bool overheal = false)
    {
        HP += amount;

        if (HP > maxHP && !overheal)
        {
            HP = maxHP;
        }

        OnHeal(amount);
    }
}
