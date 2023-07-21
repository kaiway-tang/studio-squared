using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackStateData", menuName = "Data/State Data/Attack State")]
public class D_AttackState : ScriptableObject
{
    public int damage;
    public float cooldown;
}
