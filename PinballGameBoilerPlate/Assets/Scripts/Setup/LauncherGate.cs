using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LauncherGate : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public Tunnel ballReturner;


    private void Start()
    {
        ballReturner.ballEntered.AddListener(delegate { ToggleGate(false); });
    }

    private void ToggleGate(bool on)
    {
        spriteRenderer.enabled = on;
        boxCollider.isTrigger = !on;

        if (!on)
        {
            //ScoreManager.ResetPoints();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ToggleGate(true);
        }
    }
}
