using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectCoin;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayCollectSound();
            Destroy(gameObject);
        }
    }

    private void PlayCollectSound()
    {
        if (collectCoin != null)
        {
            //audioSource.PlayOneShot(collectCoin);
            AudioSource.PlayClipAtPoint(collectCoin, transform.position);
        }
    }

}
