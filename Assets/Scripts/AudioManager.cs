using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource[] audioSrcList;
    private AudioSource backgroundAudioSrc;
    public static AudioSource pickupAudioSrc;
    public static AudioSource deathAudioSrc;
    public static AudioSource clearAudioSrc;
    public static AudioSource powerAudioSrc;
    
    // Initialisation.
    void Start()
    {
        audioSrcList = GetComponents<AudioSource>();
        backgroundAudioSrc = audioSrcList[0];
        pickupAudioSrc = audioSrcList[1];
        deathAudioSrc = audioSrcList[2];
        clearAudioSrc = audioSrcList[3];
        powerAudioSrc = audioSrcList[4];

        backgroundAudioSrc.Play();
    }
}
