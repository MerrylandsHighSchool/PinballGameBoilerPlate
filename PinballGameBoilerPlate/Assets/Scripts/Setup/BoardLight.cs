using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLight : GameComponent
{
    public Mode lightMode;
    public Color lightOnColour;
    public float onTime;
    public float delayTime;

    private SpriteRenderer _spriteRenderer;
    private Color lightOffColour;
    private float onStart;

    public enum Mode { Proximity, Blink }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        lightOffColour = _spriteRenderer.color;
        onStart = -onTime + delayTime;
    }

    void Update()
    {
        if (Time.time > onStart + onTime)
        {
            if (lightMode == Mode.Proximity)
            {
                _spriteRenderer.color = lightOffColour;
            }
            else if (lightMode == Mode.Blink)
            {
                onStart = onStart + onTime * 2;
                _spriteRenderer.color = lightOffColour;
            }
        }
        else if (Time.time > onStart)
        {
            _spriteRenderer.color = lightOnColour;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (lightMode == Mode.Proximity) {
            onStart = Time.time + delayTime;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        Score();
    }

}