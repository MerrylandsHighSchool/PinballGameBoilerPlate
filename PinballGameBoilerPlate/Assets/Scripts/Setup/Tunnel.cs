using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Tunnel : GameComponent
{
    public Tunnel exitTunnel;
    public float exitSpeed;
    public float travelTime;

    [HideInInspector]
    public UnityEvent ballEntered;

    private Collider2D _collider2D;

    private static string resetTunnelTag = "ResetTunnel";


    void Start()
    {
        if (!exitTunnel)
        {
            exitTunnel = GameObject.FindGameObjectWithTag(resetTunnelTag).GetComponent<Tunnel>();
            if (!exitTunnel) exitTunnel = this;
        }
        _collider2D = GetComponent<Collider2D>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.otherCollider == _collider2D)
        {
            Score();
            StartCoroutine(Disappear(collision.rigidbody));
            exitTunnel.StartCoroutine(exitTunnel.Appear(collision.rigidbody));
        }
    }

    public IEnumerator Disappear(Rigidbody2D rb2d)
    {
        rb2d.simulated = false;
        TrailRenderer trailRenderer = rb2d.GetComponent<TrailRenderer>();
        while (trailRenderer && trailRenderer.positionCount > 0) {
            yield return new WaitForEndOfFrame();
        }
        Destroy(rb2d.gameObject);
    }

    public IEnumerator Appear(Rigidbody2D rb2d)
    {
        ballEntered.Invoke();
        Rigidbody2D newRigidbody2D = Instantiate(rb2d.gameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();



        yield return new WaitForSeconds(travelTime);

        newRigidbody2D.simulated = true;
        newRigidbody2D.velocity = transform.up * exitSpeed;
        newRigidbody2D.GetComponent<Collider2D>().enabled = true;
    }
}
