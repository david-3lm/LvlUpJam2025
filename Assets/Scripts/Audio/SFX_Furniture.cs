using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SFX_F
{
    put,
    rotate
}

[RequireComponent(typeof(AudioSource))]
public class SFX_Furniture : MonoBehaviour
{
    [SerializeField]private AudioClip put;
    [SerializeField]private AudioClip rotate;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SFX_F f)
    {
        switch (f)
        {
            case SFX_F.put:
                audioSource.PlayOneShot(put);
                break;
            case SFX_F.rotate:
                audioSource.PlayOneShot(rotate);
                break;
            default:
                break;
        }
    }
}
