using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] protected int maxHP, HP;
    [SerializeField] protected Transform trfm;
    [SerializeField] EntityType entityType;

    public const int IGNORED = -1, ALIVE = 0, DEAD = 1;

    public enum EntityType
    {
        Enemy, Player, Neutral
    }

    protected void Start()
    {
        if (HP == 0) { HP = maxHP; }
    }

    public virtual int TakeDamage(int amount, Vector2 knockback, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        return TakeDamage(amount, entitySource, attackID);
    }

    public int TakeDamage(int amount, EntityType entitySource = EntityType.Neutral, int attackID = 0)
    {
        if (entitySource == entityType || !ValidAttackID(attackID)) { return IGNORED; }

        HP -= amount;

        GameManager.BloodFXPooler.Instantiate(trfm.position);

        if (HP < 0)
        {
            Destroy(gameObject);
            return DEAD;
        }
        return ALIVE;
    }

    int[] trackedAttackIDs = new int[3];
    int latestAttackIDIndex;
    bool ValidAttackID(int attackID) //returns False if attackID is found, True otherwise; also handles attackID tracking
    {
        bool result = !(trackedAttackIDs[0] == attackID || trackedAttackIDs[1] == attackID || trackedAttackIDs[2] == attackID);

        trackedAttackIDs[latestAttackIDIndex] = attackID;
        latestAttackIDIndex++;
        if (latestAttackIDIndex > 2) { latestAttackIDIndex  = 0; }

        return result || attackID == 0;
    }

    public void Heal(int amount, bool overheal = false)
    {
        HP += amount;

        if (HP > maxHP && !overheal)
        {
            HP = maxHP;
        }
    }
}
