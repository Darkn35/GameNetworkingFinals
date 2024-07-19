using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMechanic : MonoBehaviour
{

    public bool isHolding;
    public bool isConnected;

    public GameObject PlayerObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyHingeJoint()
    {
        Destroy(GetComponent<HingeJoint>());
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == PlayerObject)
        {
        }
        else
        {
            if (isHolding && isConnected == false)
            {
                Rigidbody rigidbodyOther = collision.transform.GetComponent<Rigidbody>();

                if (rigidbodyOther != null)
                {
                    HingeJoint joint = transform.gameObject.AddComponent(typeof(HingeJoint)) as HingeJoint;
                    joint.anchor = new Vector3(1.5f, 1.5f, 1.5f);
                    joint.connectedBody = rigidbodyOther;
                    isConnected = true;
                }
            }
        }
    }
}
