using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] private AudioSource sfxObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SFXClip(AudioClip audioClip, Transform spawnTransform)
    {
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);
    }
}
