using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Space;
    public float distance = 2;
    public float launchSpeed = 50;
    public float returnSpeed = 10;

    private bool launch;
    private Vector2 startPos;
    private Vector2 endPos;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // The default position of the launcher
        startPos = transform.position;
        // The extended position of the launcher
        endPos = new Vector2(transform.position.x, transform.position.y - distance);

        launch = false;
    }

    void Update()
    {
        // If the input key is down, move the launcher from the default position to the extended position with a bit of maths to exaggerate tension
        if (Input.GetKey(inputKey)) transform.position = Vector2.MoveTowards(transform.position, endPos, returnSpeed * Time.deltaTime * Mathf.Abs(transform.position.y - endPos.y) / distance);
        // Otherwise launch
        else launch = true;
    }

    void FixedUpdate()
    {
        // If the launcher has just been released
        if (launch && rb2d.velocity.y == 0)
        {
            rb2d.velocity = new Vector2(0, (Vector2.Distance(transform.position, startPos) / distance) * launchSpeed);
        }
    }

    void LateUpdate()
    {
        // Arrest the launcher if it has finished launching
        if (transform.position.y >= startPos.y)
        {
            launch = false;
            transform.position = startPos;
            rb2d.velocity = Vector2.zero;
        }
    }
}
