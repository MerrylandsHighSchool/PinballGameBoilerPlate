using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Flipper : GameComponent
{
    public KeyCode input;
    public float speed;

    [Range(-360f, 360f)]
    public float range;

    private Rigidbody2D rb2d;
    private Vector2 closedVector;
    private bool opening;

    public Vector2 ClosedVector { get { return EditorApplication.isPlaying ? closedVector * Mathf.Sign(transform.localScale.y) : (Vector2)transform.up * Mathf.Sign(transform.localScale.y); } }
    public Vector2 OpenVector { get { return EditorApplication.isPlaying ? Quaternion.AngleAxis(range, transform.forward) * closedVector : Quaternion.AngleAxis(range, transform.forward) * transform.up; } }

    // Initialise variables
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.centerOfMass = Vector2.zero;

        closedVector = transform.up;
        
        opening = false;
    }

    // Check for input
    void Update() {
        opening = Input.GetKey(input);
    }

    void FixedUpdate() {
        // Determine which way to move the flipper and calculate the distance from the target rotation
        Vector2 targetVector = opening ? OpenVector : closedVector;
        float angleBetween = Vector3.Angle(transform.up, targetVector);

        // Rotates anti-clockwise by default, is inverted when closing and inverted when set to open clockwise
        bool opensClockwise = range < 0;
        angleBetween *= opensClockwise && opening || !opensClockwise && !opening ? -1 : 1;
        
        if (speed * Time.deltaTime > Mathf.Abs(angleBetween)) {
            transform.up = targetVector;
            angleBetween = 0;
        }

        rb2d.angularVelocity = MySign(angleBetween) * speed;
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        Score();
    }

    // Similar to Mathf.Sign but 0 returns 0 rather than 1
    int MySign(float num) {
        if (num > 0) return 1;
        if (num < 0) return -1;
        return 0;
    }
}
