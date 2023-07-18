using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject
{
    public float moveSpeed = 3f;
    public float maxSpeed = 5f;
}
