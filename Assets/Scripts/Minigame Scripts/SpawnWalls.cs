using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class SpawnWalls : MonoBehaviour
{
    [Header("Wall Spawnpoints")]
    public Transform northSpawn;
    public Transform southSpawn;
    public Transform eastSpawn;
    public Transform westSpawn;

    [Header("Wall Prefab Variations")]
    public GameObject[] wallPrefabs;

    [Header("Wall Properties")]
    public float wallSpeed;
    public float spawnDelay;
    public int wallsToSpawn;
    public int spawnCount;

    private Transform transformTarget;
    public bool canSpawn = false;
    public bool isGameStarted = false;
    public EliminationZone eliminationZone;

    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.AddComponent<PhotonView>();
        view = GetComponent<PhotonView>();
    }

    public void RestartGameRPC()
    {
        view.RPC("RestartGame", RpcTarget.All);
    }

    public void StartMinigameRPC()
    {
        if (view.IsMine)
        {
            //view.RPC("StartMiniGame", RpcTarget.All);
            StartMiniGame();
            eliminationZone.playersTotal = PhotonNetwork.PlayerList.Length;
        }
    }

    [PunRPC]
    public void RestartGame()
    {
        PhotonNetwork.LoadLevel("Minigame Level");
    }

    [PunRPC]
    public void StartMiniGame()
    {
        if (!isGameStarted)
        {
            canSpawn = true;
            isGameStarted = true;
        }
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    private IEnumerator SpawnDelay()
    {
        canSpawn = false;
        //SpawnPrefabWall();

        view.RPC("SpawnPrefabWall", RpcTarget.All);

        yield return new WaitForSeconds(spawnDelay);

        canSpawn = true;
    }

    [PunRPC]
    void SpawnPrefabWall()
    {
        if (view.IsMine)
        {
                int randomSpawnLocation = Random.Range(1, 5);
                int randomWallPrefab = Random.Range(0, wallPrefabs.Length);

                Vector3 location = new Vector3(0f, 0f, 0f);
                Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
                bool isFlipped = false;
                //transformTarget = null;

                float randomOrientation = Random.Range(0.0f, 100.0f);


                if (randomOrientation <= 50.0f)
                {
                    isFlipped = true;
                }

                switch (randomSpawnLocation)
                {
                    case 1:
                        {
                            location = northSpawn.position;
                            rotation = northSpawn.rotation;
                            transformTarget = southSpawn;
                            break;
                        }
                    case 2:
                        {
                            location = southSpawn.position;
                            rotation = southSpawn.rotation;
                            transformTarget = northSpawn;
                            break;
                        }
                    case 3:
                        {
                            location = eastSpawn.position;
                            rotation = eastSpawn.rotation;
                            transformTarget = westSpawn;
                            break;
                        }
                    case 4:
                        {
                            location = westSpawn.position;
                            rotation = westSpawn.rotation;
                            transformTarget = eastSpawn;
                            break;
                        }
                }


                //GameObject spawnedWall = Instantiate(wallPrefabs[SetRandomwWallProperties()], location, rotation, this.gameObject.transform);
                GameObject spawnedWall = PhotonNetwork.Instantiate(wallPrefabs[randomWallPrefab].name, location, rotation);
                spawnedWall.GetComponent<WallBehavior>().speed = wallSpeed;

                try
                {
                    spawnedWall.GetComponent<WallBehavior>().target = transformTarget;
                }
                catch { }

                if (isFlipped)
                {
                    float originalY = Mathf.Ceil(spawnedWall.transform.rotation.y + 180);
                    spawnedWall.transform.Rotate(0, originalY, 0);
                }
                spawnedWall.GetComponent<WallBehavior>().isInitialized = true;
        }
    }
}
