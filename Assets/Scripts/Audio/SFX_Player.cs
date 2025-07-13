using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX_P
{
    crash = 1,
    clean = 2,
    gameOver = 3,
    win = 4,
    start = 5
}
public class SFX_Player : MonoBehaviour
{
    [SerializeField]private AudioClip crash;
    [SerializeField]private AudioClip clean;
    [SerializeField]private AudioClip gameOver;
    [SerializeField]private AudioClip win;
    [SerializeField]private AudioClip start;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SFX_P p)
    {
        switch (p)
        {
            case SFX_P.crash:
                audioSource.PlayOneShot(crash);
                break;
            case SFX_P.clean:
                audioSource.PlayOneShot(clean);
                break;
            case SFX_P.gameOver:
                audioSource.PlayOneShot(gameOver);
                break;
            case SFX_P.win:
                audioSource.PlayOneShot(win);
                break;
            case SFX_P.start:
                audioSource.PlayOneShot(start);
                break;
            default:
                break;
        }
    }
}
