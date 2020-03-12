using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : GameComponent
{
    Vector3 scale;
    float scalePercent = 1;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Score();
        scalePercent = 1.2f;
    }

    private void Start()
    {
        scale = transform.localScale;

    }

    private void Update()
    {
        if (scalePercent > 1)
            scalePercent = Mathf.Max(1, scalePercent - Time.deltaTime);
        transform.localScale = scale * scalePercent;
    }
}
