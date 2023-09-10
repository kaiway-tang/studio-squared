using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static KeyCode JumpKey, JumpKey1, UpKey, UpKey1, DownKey, DownKey1, LeftKey, LeftKey1, RightKey, RightKey1;
    public static KeyCode AttackKey, AttackKey1, DashKey, DashKey1, CastKey, CastKey1;
    public static KeyCode InteractKey;

    static bool firstLoad;

    private void Start()
    {
        if (firstLoad) { return; }

        JumpKey = KeyCode.Space;
        //JumpKey1 = KeyCode.W;

        UpKey = KeyCode.W;
        UpKey1 = KeyCode.UpArrow;

        DownKey = KeyCode.S;
        DownKey1 = KeyCode.DownArrow;

        LeftKey = KeyCode.A;
        LeftKey1 = KeyCode.LeftArrow;

        RightKey = KeyCode.D;
        RightKey1 = KeyCode.RightArrow;


        AttackKey = KeyCode.U;
        AttackKey1 = KeyCode.V;

        DashKey = KeyCode.I;
        DashKey1 = KeyCode.C;

        CastKey = KeyCode.O;
        CastKey1 = KeyCode.X;

        InteractKey = KeyCode.E;

        firstLoad = true;
    }

    public bool RebindKey(KeyCode key)
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < 510; i++)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    key = (KeyCode)i;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool JumpPressed()
    {
        return Input.GetKeyDown(JumpKey) || Input.GetKeyDown(JumpKey1);
    }
    public static bool JumpHeld()
    {
        return Input.GetKey(JumpKey) || Input.GetKey(JumpKey1);
    }
    public static bool JumpReleased()
    {
        return Input.GetKeyUp(JumpKey) || Input.GetKeyUp(JumpKey1);
    }

    public static bool UpPressed()
    {
        return Input.GetKeyDown(UpKey) || Input.GetKeyDown(UpKey1);
    }
    public static bool UpHeld()
    {
        return Input.GetKey(UpKey) || Input.GetKey(UpKey1);
    }
    public static bool UpReleased()
    {
        return Input.GetKeyUp(UpKey) || Input.GetKeyUp(UpKey1);
    }

    public static bool DownPressed()
    {
        return Input.GetKeyDown(DownKey) || Input.GetKeyDown(DownKey1);
    }
    public static bool DownHeld()
    {
        return Input.GetKey(DownKey) || Input.GetKey(DownKey1);
    }
    public static bool DownReleased()
    {
        return Input.GetKeyUp(DownKey) || Input.GetKeyUp(DownKey1);
    }

    public static bool LeftPressed()
    {
        return Input.GetKeyDown(LeftKey) || Input.GetKeyDown(LeftKey1);
    }
    public static bool LeftHeld()
    {
        return Input.GetKey(LeftKey) || Input.GetKey(LeftKey1);
    }
    public static bool LeftReleased()
    {
        return Input.GetKeyUp(LeftKey) || Input.GetKeyUp(LeftKey1);
    }

    public static bool RightPressed()
    {
        return Input.GetKeyUp(RightKey) || Input.GetKeyUp(RightKey1);
    }
    public static bool RightHeld()
    {
        return Input.GetKey(RightKey) || Input.GetKey(RightKey1);
    }
    public static bool RightReleased()
    {
        return Input.GetKeyDown(RightKey) || Input.GetKeyDown(RightKey1);
    }

    public static bool AttackPressed()
    {
        return Input.GetKeyDown(AttackKey) || Input.GetKeyDown(AttackKey1);
    }
    public static bool AttackHeld()
    {
        return Input.GetKey(AttackKey) || Input.GetKey(AttackKey1);
    }
    public static bool AttackReleased()
    {
        return Input.GetKeyUp(AttackKey) || Input.GetKeyUp(AttackKey1);
    }

    public static bool DashPressed()
    {
        return Input.GetKeyDown(DashKey) || Input.GetKeyDown(DashKey1);
    }
    public static bool DashHeld()
    {
        return Input.GetKey(DashKey) || Input.GetKey(DashKey1);
    }


    public static bool CastPressed()
    {
        return Input.GetKeyDown(CastKey) || Input.GetKeyDown(CastKey1);
    }
    public static bool CastHeld()
    {
        return Input.GetKey(CastKey) || Input.GetKey(CastKey1);
    }

    static Vector2 vect2;
    public static Vector2 GetVectorInput()
    {
        if (UpHeld())
        {
            if (LeftHeld())
            {
                vect2.x = -.707f;
                vect2.y = .707f;
            }
            else if (RightHeld())
            {
                vect2.x = .707f;
                vect2.y = .707f;
            }
            else
            {
                vect2.x = 0;
                vect2.y = 1;
            }
        }
        else if (DownHeld())
        {
            if (LeftHeld())
            {
                vect2.x = -.707f;
                vect2.y = -.707f;
            }
            else if (RightHeld())
            {
                vect2.x = .707f;
                vect2.y = -.707f;
            }
            else
            {
                vect2.x = 0;
                vect2.y = -1;
            }
        }
        else
        {
            if (LeftHeld())
            {
                vect2.x = -1;
                vect2.y = 0;
            }
            else if (RightHeld())
            {
                vect2.x = 1;
                vect2.y = 0;
            }
            else
            {
                return Vector2.zero;
            }
        }

        return vect2;
    }
}
