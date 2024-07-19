using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject players;
    public Transform spawnPoint;

    PlayerFollow playerFollow;

    private void Awake()
    {
        playerFollow = FindAnyObjectByType<PlayerFollow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayers()
    {
        GameObject player = PhotonNetwork.Instantiate(players.name, spawnPoint.position, spawnPoint.rotation);
        //GameObject player = Instantiate(players, spawnPoint.position, spawnPoint.rotation);
        playerFollow.SetCameraTarget(player.transform);
    }
}
