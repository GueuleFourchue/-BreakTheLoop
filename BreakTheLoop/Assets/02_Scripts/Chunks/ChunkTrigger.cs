using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour {

    public Vector3 newAddedPosition;
    public Transform chunk;
    public GameObject chunkPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine (MoveChunk(other.transform));
        }
    }

    IEnumerator MoveChunk(Transform player)
    {
        //Get Player direction, to know where to put the chunk
        Vector2 triggerDirection = new Vector2(this.transform.forward.x, this.transform.forward.z);

        Vector2 playerDirection = new Vector2 (player.position.x, player.position.z);
        yield return new WaitForSeconds(0.1f);
        playerDirection = (new Vector2(player.position.x, player.position.z) - playerDirection).normalized;

        float dot = Vector2.Dot(triggerDirection, playerDirection);

        
        //GameObject obj = GameObject.Instantiate(chunkPrefab);

        //Move Chunk
        if (dot > 0)
        {
            /*
            obj.transform.position =  chunk.position += newAddedPosition;
            Destroy(chunk.gameObject);
            */
            chunk.position += newAddedPosition;
        }
        else
        {
            /*
            obj.transform.position = chunk.position -= newAddedPosition;
            Destroy(chunk.gameObject);
            */
            chunk.position -= newAddedPosition;
        }
    }
}
