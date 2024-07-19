using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Username : MonoBehaviour
{
    public TMP_InputField inputField;

    public void SaveUsername()
    {
        PhotonNetwork.NickName = inputField.text;
        PlayerPrefs.SetString("Username", inputField.text);
    }
}
