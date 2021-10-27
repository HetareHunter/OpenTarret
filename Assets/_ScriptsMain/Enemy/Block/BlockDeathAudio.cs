using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlockDeathAudio : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClips;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayDeathSound()
    {
        var randomNom = Random.Range(0, _audioClips.Length);
        _audioSource.PlayOneShot(_audioClips[randomNom]);
    }
}
