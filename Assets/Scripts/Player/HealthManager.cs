using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("References")]
    public PlayerController pc;

    [Header("List")]
    public Image[] hearts;

    [Header("Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < pc.lives; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

}
