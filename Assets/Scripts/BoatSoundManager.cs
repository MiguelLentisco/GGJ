using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoatSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;

    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip winsound;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        source.PlayOneShot(explosion);

    }
    
    public void PlayWinpoint()
    {
        source.PlayOneShot(winsound);
    }

    public void Update()
    {
        if (!source.isPlaying)
            Destroy(this.gameObject);
    }
}
