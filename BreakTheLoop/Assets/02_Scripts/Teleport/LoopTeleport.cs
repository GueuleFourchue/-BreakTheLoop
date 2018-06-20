using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

public class LoopTeleport : MonoBehaviour {

    public Transform player;
    public SoundEffects soundEffects;

    [HideInInspector]
    public Vector3 finalPosition;

    public IEnumerator Teleport()
    {
        player.transform.position = finalPosition;
        soundEffects.PlaySoundOnce(soundEffects.glitch);
        Camera.main.GetComponent<Glitch>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        Camera.main.GetComponent<Glitch>().enabled = false;
    }
}
