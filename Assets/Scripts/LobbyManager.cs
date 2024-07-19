using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{ 
    public TMP_InputField createInput, joinInput, usernameInput;
    public byte maxPlayers;

    public void CreateButton()
    {
        if(CheckForUsername())
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions);
        }
    }

    public void JoinButton()
    {
        if (CheckForUsername()) { PhotonNetwork.JoinRoom(joinInput.text); }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Minigame Level");
    }

    private bool CheckForUsername()
    {
        string username = usernameInput.text;
        if (!string.IsNullOrEmpty(username)) { return true; }
        else { Debug.Log("Please enter your Username"); return false; }
    }
}
