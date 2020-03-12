using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public ParticleSystem particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject particleInstance = Instantiate(particleEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(particleInstance, particleEffect.main.duration -0.01f);
        }
    }
}
