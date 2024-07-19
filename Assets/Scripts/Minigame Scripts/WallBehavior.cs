using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Transform target;
    private float stoppingDistance = 0.1f;

    Rigidbody rb;
    public Vector3 directionOfTravel;
    public PhotonView view;
    public bool isInitialized = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        //transform.LookAt(target);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInitialized)
        {
            Vector3 targetPostion = target.position;
            Vector3 currentPosition = transform.position;

            float distance = Vector3.Distance(currentPosition, targetPostion);

            if (distance > stoppingDistance)
            {
                directionOfTravel = targetPostion - currentPosition;
                directionOfTravel.Normalize();

                rb.MovePosition(currentPosition + (directionOfTravel * speed * Time.deltaTime));
            }
            else
            {
                if (view.IsMine)
                {
                    view.RPC("DeactivateWall", RpcTarget.All);
                }
            }
        }
        //var step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    [PunRPC]
    public void DeactivateWall()
    {
        this.gameObject.SetActive(false);
    }

    public void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        //if (collision.gameObject.TryGetComponent<CharacterController>(out CharacterController playerCharacterController))
        //{
        //    playerCharacterController.SimpleMove(directionOfTravel * 1f);
        //}

        Debug.Log("Stay");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
    }

    private void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.gameObject.transform.parent.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.PushAwayFromWall(directionOfTravel);
            }
        }
        catch { }
    }
}
