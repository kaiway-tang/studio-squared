using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStateEntityData", menuName = "Data/Entity Date/Base Data")]
public class D_StateEntity : ScriptableObject
{
    public float maxAggroDistance = 4f;
    public float minAggroDistance = 3;
    public float attackDistance = 1f;
}
