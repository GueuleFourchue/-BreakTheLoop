using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    [Header("FootsSteps")]
    public AudioSource footSteps_audioSource;
    public AudioClip[] footSteps_SFX;
    public float footSteps_Delay;
    public bool isWalking;

    [Header("Glitch")]
    public AudioSource glitch;

    [Header("Jump")]
    public AudioSource jump;

    [Header("PickUp")]
    public AudioSource pickup;

    [HideInInspector]
    public float footSteps_Timer;

	void Update ()
    {
		if (isWalking)
        {
            footSteps_Timer += Time.deltaTime;
        }

        if (footSteps_Timer > footSteps_Delay)
        {
            footSteps_Timer = 0;
            PlayFootsteps(footSteps_audioSource, 0.5f);
        }
	}

    void PlayFootsteps(AudioSource audioS, float volume)
    {
        //GetRandomClip
        int index = Random.Range(0, footSteps_SFX.Length);
        AudioClip clip = footSteps_SFX[index];
        audioS.clip = clip;

        //PlaySound
        audioS.pitch = Random.Range(0.95f, 1.05f);
        audioS.volume = Random.Range(volume - 0.05f, volume + 0.05f);
        audioS.Play();
    }

    public void PlaySoundOnce(AudioSource audioS)
    {
        audioS.Play();
    }
}
