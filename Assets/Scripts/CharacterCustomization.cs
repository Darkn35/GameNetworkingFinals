using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class CharacterCustomizer : MonoBehaviour
{
    //public GameObject[] pants;
    public AvatarSetUp avatar;

    // Player Hair / Hat
    [Header("Player Hat / Hair")]
    private GameObject[] hats;
    public int hairIndexData;
    private int hatParam;

    // Player Chest
    [Header("Player Chest")]
    private GameObject[] chests;
    public int chestIndexData;

    // Player Legs
    [Header("Player Legs")]
    private GameObject[] legs;
    public int legsIndexData;

    private PhotonHashtable hash = new PhotonHashtable();

    private void Awake()
    {
        ChangeHats(0);
        ChangeChests(0);
        ChangeLegs(0);
    }

    public void Update()
    {
        //ChangeHats(hatParam);
    }
    public void ChangeHats(int parameter)
    {
        avatar.hairIndex += parameter;
        PlayerData.instance.data.playerHat = avatar.CurrentHairIndex;

        if (PlayerData.instance.data.playerHat < 0 || PlayerData.instance.data.playerHat >= avatar.hairStyle.Length)
        {
            PlayerData.instance.data.playerHat = 0;
        }

        hairIndexData = PlayerData.instance.data.playerHat;
        PlayerPrefs.SetInt("Hair", PlayerData.instance.data.playerHat);
        avatar.SetAvatar(PlayerData.instance.data);

        if (PhotonNetwork.IsConnected)
        {
            SetHash();
        }
    }

    public void ChangeChests(int parameter)
    {
        avatar.chestIndex += parameter;
        PlayerData.instance.data.playerChest = avatar.CurrentChestIndex;

        if (PlayerData.instance.data.playerChest < 0 || PlayerData.instance.data.playerChest >= avatar.chestStyle.Length)
        {
            PlayerData.instance.data.playerChest = 0;
        }

        chestIndexData = PlayerData.instance.data.playerChest;
        PlayerPrefs.SetInt("Chest", PlayerData.instance.data.playerChest);
        avatar.SetAvatar(PlayerData.instance.data);

        if (PhotonNetwork.IsConnected)
        {
            SetHash();
        }
    }

    public void ChangeLegs(int parameter)
    {
        avatar.legsIndex += parameter;
        PlayerData.instance.data.playerLegs = avatar.CurrentLegIndex;

        if (PlayerData.instance.data.playerLegs < 0 || PlayerData.instance.data.playerLegs >= avatar.leftLegs.Length)
        {
            PlayerData.instance.data.playerLegs = 0;
        }

        legsIndexData = PlayerData.instance.data.playerLegs;
        PlayerPrefs.SetInt("Legs", PlayerData.instance.data.playerLegs);
        avatar.SetAvatar(PlayerData.instance.data);

        if (PhotonNetwork.IsConnected)
        {
            SetHash();
        }
    }

    public void SetHash()
    {
        hash["Hair"] = hairIndexData.ToString();
        hash["Chest"] = chestIndexData.ToString();
        hash["Legs"] = legsIndexData.ToString();
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}