using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangePlatform : MonoBehaviour
{
        public Color newColor = Color.red; // Цвет, в который будет изменена платформа при наступлении
    private Color baseColor; // Базовый цвет платформы
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Platform: No SpriteRenderer found on the platform object.");
        }
        else
        {
            baseColor = spriteRenderer.color; // Сохраните базовый цвет платформы
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeColor(newColor);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeColor(baseColor);
        }
    }

    private void ChangeColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
}
