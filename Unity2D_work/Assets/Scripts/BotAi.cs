using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAi : MonoBehaviour
{
    public float speed = 2f; // Скорость бота
    public float chaseRadius = 5f; // Радиус преследования
    public AudioClip chaseSound; // Звук преследования

    private Transform player;
    private AudioSource audioSource;
    private bool isChasing = false;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("BotAI: No SpriteRenderer");
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRadius)
        {
            StartChasing();
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        // Разворот бота в сторону игрока
        if (spriteRenderer != null)
        {
            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void StartChasing()
    {
        if (!isChasing)
        {
            isChasing = true;
            PlayChaseSound();
        }
    }

    private void StopChasing()
    {
        if (isChasing)
        {
            isChasing = false;
            StopChaseSound();
        }
    }

    private void PlayChaseSound()
    {
        if (audioSource != null && chaseSound != null)
        {
            audioSource.loop = true;
            audioSource.clip = chaseSound;
            audioSource.Play();
        }
    }

    private void StopChaseSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
