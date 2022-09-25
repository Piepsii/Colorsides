using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Color defaultColor;
    public Color stickyColor;
    public Color goalColor;
    public bool isSticky;
    public bool isGoal;

    public SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        if (isSticky)
        {
            spriteRenderer.color = stickyColor;
        }
        else if(isGoal)
        {
            spriteRenderer.color = goalColor;
        }
        else
        {
            spriteRenderer.color = defaultColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if(player != null && isGoal)
        {
            GameManager.instance.LevelOver();
        }
    }
}
