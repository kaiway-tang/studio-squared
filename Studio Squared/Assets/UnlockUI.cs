using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockUI : MonoBehaviour
{
    [SerializeField] Fader fader;
    [SerializeField] SpriteRenderer spriteRenderer;
    static UnlockUI self;
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<UnlockUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowText(Sprite sprite) //ID based on Pickup const's
    {
        self.spriteRenderer.sprite = sprite;
        self.fader.FadeIn(.01f);
    }

    public static void ClearText()
    {
        self.fader.FadeOut(.02f);
    }
}
