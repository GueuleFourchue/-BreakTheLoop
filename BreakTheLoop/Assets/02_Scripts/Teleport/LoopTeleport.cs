using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colorful;

public class LoopTeleport : MonoBehaviour {

    public Transform player;
    public SoundEffects soundEffects;

    Vector3 originPlayerPosition;

    private void Start()
    {
        originPlayerPosition = player.position;
    }

    public IEnumerator Teleport()
    {
        player.transform.position = new Vector3(player.transform.position.x, originPlayerPosition.y, originPlayerPosition.z);
        soundEffects.PlaySoundOnce(soundEffects.glitch);
        Camera.main.GetComponent<Glitch>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        Camera.main.GetComponent<Glitch>().enabled = false;
    }
}
