using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiedSound : MonoBehaviour
{
    [SerializeField]AudioSource _AudioSource;
    [SerializeField]AudioClip _sound;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SoundPlay", .1f);
    }
    void SoundPlay()
    {
        _AudioSource.PlayOneShot(_sound);
    }
}
