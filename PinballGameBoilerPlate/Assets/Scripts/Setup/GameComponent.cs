using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComponent : MonoBehaviour
{
    [SerializeField] private int pointValue = 0;
    [SerializeField] private AudioClip soundEffect;
    private AudioSource audioSource;


    private void Awake()
    {
        if(soundEffect != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundEffect;
        }
    }

    public void Score()
    {
        ScoreManager.AddPoints(pointValue);
    }

    public void ResetPoints()
    {
        ScoreManager.ResetPoints();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Play sound
            if(audioSource != null)
                audioSource.Play();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            //Play sound
            if (audioSource != null)
                audioSource.Play();
        }
    }
}
