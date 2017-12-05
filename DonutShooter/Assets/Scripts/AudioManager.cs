using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource audi;
    public AudioClip bgmSound;
    public AudioClip hitSound;
    public AudioClip shootSound;
    public AudioClip getHeartSound;
    public AudioClip dedSound;
    public AudioClip reloadSound;
    public AudioClip explodeSound;

	// Audio manager, all audio are in here.
	void Start () {
        audi = gameObject.GetComponent<AudioSource>();

	}

    public void hit()
    {
        audi.PlayOneShot(hitSound);
    }

    public  void shoot()
    {
        audi.PlayOneShot(shootSound);
    }
    public  void getheart()
    {
        audi.PlayOneShot(getHeartSound);
    }
    public void ded()
    {
        audi.PlayOneShot(dedSound);
    }
    public void reload()
    {
        audi.PlayOneShot(reloadSound);
    }
    public void explode()
    {
        audi.PlayOneShot(explodeSound);
    }

}
