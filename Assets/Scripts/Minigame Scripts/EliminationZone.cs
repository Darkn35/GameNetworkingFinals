using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EliminationZone : MonoBehaviour
{
    // Start is called before the first frame update

    public SpawnWalls spawnWalls;

    public int playersEliminated = 0;
    public int playersTotal = 0;

    public PhotonView view;

    public void OnTriggerEnter(Collider other)
    {
        // Elimination of player code

        if (view.IsMine)
        {
            if (other.gameObject.CompareTag("PlayerTag"))
            {
                //other.gameObject.SetActive(false);

                other.gameObject.GetComponent<PlayerController>().EliminatePlayer();

                playersEliminated++;

                //if (playersEliminated >= playersTotal)
                //{
                //    Debug.Log("Game Finished");
                //    spawnWalls.canSpawn = false;
                //}
            }
        }
    }
}
