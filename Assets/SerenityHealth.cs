using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SerenityHealth : MonoBehaviour
{
    public Slider healthIndicator;
    public float health = 1000;
    public float maxHealth;
    public ParticleSystem explosion;
    public AudioSource audioSource;
    public AudioClip bigNoise;

    public Controls nave;

    public TextMeshProUGUI endPondCount;
    public TextMeshProUGUI podCountDisplay;
    public GameObject gameUi;
    public GameObject gameOver;

    public int podCount=0;

    public bool gameIsRunning = false;

    public DialogueController game;


    // Start is called before the first frame update
    void Start()
    {
        healthIndicator.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsRunning) { return; }
        healthIndicator.value = health;
        podCountDisplay.text = "Saved Pods: " + podCount;
        endPondCount.text = "You have saved "+podCount+" pods";
        if (health <= 0f)
        {
            nave.EndGame();
            StartCoroutine(Die());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!gameIsRunning) { return; }
        health--;
        Meteor m = collision.gameObject.GetComponent<Meteor>();
        if (m != null)
        {
            m.Die();
        }
        
    }
    public void PodSaved()
    {
        podCount++;
    }
    public void PodCreated()
    {
        health--;
    }
    IEnumerator Die()
    {
        gameUi.SetActive(false);
        gameOver.SetActive(true);
        
        yield return new WaitForSeconds(5f);
        ParticleSystem bum = Instantiate(explosion);
        bum.transform.position = transform.position;
        audioSource.PlayOneShot(bigNoise, 1.6f);
        game.stage = 5;
        Destroy(gameObject); ;
    }
}
