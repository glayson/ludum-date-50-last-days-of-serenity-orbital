using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public ParticleSystem explosion;
    public int health = 10;
    public Rigidbody rb;
    Collider coll;
    public Vector3 rotation;
    public Vector3 direction;
    public Transform target;
    public AudioSource audioSource;
    public List<AudioClip> hitClips;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > 300f) {
            Destroy(gameObject);
        }
        
    }
    private void OnParticleCollision(GameObject other)
    {
        health--;
        if (hitClips.Count > 0)
        {
            int myRandomIndex = Random.Range(0, hitClips.Count);
            audioSource.PlayOneShot(hitClips[myRandomIndex]);
        }
        if (health <= 0)
        {
            Die();
        }
       
    }
    public void Die()
    {
        ParticleSystem bum = Instantiate(explosion);
        bum.gameObject.transform.position = transform.position;
        Destroy(gameObject);
    }

}
