using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : GameComponent
{
    public KeyCode inputKey;
    public float distance;
    public float returnSpeed;
    public float launchSpeed;


    private bool launch;
    private Vector2 startPos;
    private Vector2 endPos;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // The default position of the plunger
        startPos = transform.position;
        // The extended position of the plunger
        endPos = transform.position - transform.up * distance;

        launch = false;
    }

    void Update() {
        // If the input key is down, move the plunger from the default position to the extended position with a bit of maths to exaggerate tension
        if (Input.GetKey(inputKey)) transform.position = Vector2.MoveTowards(transform.position, endPos, returnSpeed * Time.deltaTime * Vector2.Distance(transform.position, endPos) / distance);
        // Otherwise launch
        else launch = true;
    }

    void FixedUpdate() {
        // If the plunger has just been released
        if (launch && rb2d.velocity.magnitude == 0) {
            // give the plunger velocity in the local up direction, greater the farther it is from the resting position
            rb2d.velocity = transform.up * (Vector2.Distance(transform.position, startPos) / distance) * launchSpeed;
        }
    }

    void LateUpdate() {
        // Arrest the plunger if it has finished launching
        if ((startPos - (Vector2)transform.position).normalized != (Vector2)transform.up) {
            launch = false;
            transform.position = startPos;
            rb2d.velocity = Vector2.zero;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        // If the plunger is currently moving, do the point scoring stuff
        if (rb2d.velocity.magnitude > 0) {
            ResetPoints();
            Score();
        }
    }
}
