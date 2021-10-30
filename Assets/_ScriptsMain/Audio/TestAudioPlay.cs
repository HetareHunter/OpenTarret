using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class TestAudioPlay : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] AudioClip _audioClips;
    [SerializeField] float _audioPitch = 70.0f;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_audioMixer = _audioSource.outputAudioMixerGroup.audioMixer;
    }


#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_audioMixer != null)
            {
                _audioMixer.SetFloat("BlockSEPitch", _audioPitch);
            }
            _audioSource.PlayOneShot(_audioClips);
        }
    }
#endif
}
