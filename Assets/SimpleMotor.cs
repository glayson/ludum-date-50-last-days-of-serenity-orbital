using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMotor : MonoBehaviour
{


    public Vector3 target = Vector3.zero;
    public float force = 100f;
    public float maxVelocity = 100f;
    Rigidbody rb;
    public SerenityHealth serenity;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.AddForce(direction * force * Time.deltaTime, ForceMode.Impulse);
        transform.LookAt(target);
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            serenity.PodSaved();
            Destroy(gameObject);
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(
                Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity),
                Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity),
                Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity)
            );
    }
    private void OnParticleCollision(GameObject other)
    {
        Die();

    }
    private void OnCollisionEnter(Collision collision)
    {
        Die();
        Debug.Log(collision.transform.name);
    }
    public void Die()
    {
        ParticleSystem bum = Instantiate(explosion);
        bum.gameObject.transform.position = transform.position;
        Destroy(gameObject);
    }
}
