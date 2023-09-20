using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTip : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite alternateSprite;
    [SerializeField] Transform textTrfm;

    bool inTrigger;
    void FixedUpdate()
    {
        if (inTrigger)
        {
            if (Player.self.rb.velocity.y < 0)
            {
                Player.self.SetYVelocity(0);
                Player.self.animator.RequestAnimatorState(Player.self.animator.Fall);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            if (TutorialManager.usingArrows && alternateSprite) { spriteRenderer.sprite = alternateSprite; }
            textTrfm.position = new Vector3(textTrfm.position.x, GameManager.playerTrfm.position.y + 2, 0);
            spriteRenderer.enabled = true;
            inTrigger = true;
            Time.timeScale = .5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.PlayerTriggerLayer)
        {
            spriteRenderer.enabled = false;
            inTrigger = false;
            Time.timeScale = 1;
        }
    }
}
