using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRemove : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
        if (clips.Count > 0)
        {
            int myRandomIndex = Random.Range(0, clips.Count);
            audioSource.PlayOneShot(clips[myRandomIndex],.3f);
        }

        Destroy(gameObject, 1f);
    }

}
