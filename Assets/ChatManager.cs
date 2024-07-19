using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject messagePrefab;
    public GameObject content;

    public void SendMessage()
    {
        string message = inputField.text;
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName + " : " +  message));
    }

    [PunRPC]
    public void GetMessage(string receiveMessage)
    {
        GameObject messageObject = Instantiate(messagePrefab, Vector3.zero, Quaternion.identity, content.transform);
        messageObject.GetComponent<MessageChat>().MyMessage.text = receiveMessage;
    }
}
