using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public ParticleSystem lazer;

    bool fire = true;

    public ParticleSystem lazer2;

    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.EmissionModule emissionModule2;
    public AudioSource audioSource;
    public AudioClip shot;
    public float lastPlayed = 0f;

    float fireStarter = 0f;

    public float fireRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        emissionModule = lazer.emission;
        emissionModule2 = lazer2.emission;
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        lastPlayed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //emissionModule.rateOverTime = fireRate;
        //emissionModule2.rateOverTime = fireRate;

        if (fire)
        {
            if (Time.time > (lastPlayed + 1f / fireRate)){
                audioSource.PlayOneShot(shot,.1f);
                lastPlayed = Time.time;
            }
        }

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            StartFiring();
        }else if(!Input.GetButton("Fire1") && !Input.GetButton("Jump"))
        {
            StopFiring();
        }
    }
    void StartFiring()
    {
        if (!fire)
        {
            lazer.Play();
            fire = true;
        }
        
    }
    void StopFiring()
    {
        if (fire)
        {
            lazer.Stop();
            fire = false;
        }
    }
}
