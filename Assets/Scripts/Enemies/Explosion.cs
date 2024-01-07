using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    ExplosionManager explosionManager;

    public void InjectPool(ExplosionManager em)
    {
        explosionManager = em;
    }

    public void Play()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PlayExplosionSound());
    }

    IEnumerator PlayExplosionSound()
    {
        //  explosionSound.pitch = Random.Range(explosionSoundPitchRange.x, explosionSoundPitchRange.y);
        audioSource.Play();

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        explosionManager.ReturnToPool(this);
    }
}