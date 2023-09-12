using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    static Color color;

    public static bool InBoxRange(Vector2 pos1, Vector2 pos2, float distance)
    {
        return Mathf.Abs(pos1.x - pos2.x) < distance && Mathf.Abs(pos1.y - pos2.y) < distance;
    }

    public static float AddAlpha(SpriteRenderer spriteRenderer, float amount, bool bounded = true)
    {
        color.a = amount;
        spriteRenderer.color += color;

        if (spriteRenderer.color.a > 1)
        {
            color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
            color = Color.black;
        }
        else if (spriteRenderer.color.a < 0)
        {
            color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
            color = Color.black;
        }

        return spriteRenderer.color.a;
    }
}
