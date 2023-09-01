using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] Elevator.Use use;
    [SerializeField] Elevator elevator;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 11)
        {
            if (use == Elevator.Use.Parenter)
            {
                GameManager.playerTrfm.parent = elevator.transform;
            } else
            {
                elevator.Trigger(use);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 11 && use == Elevator.Use.Parenter)
        {
            GameManager.playerTrfm.parent = null;
        }
    }
}
