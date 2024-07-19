using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSetUp : MonoBehaviour
{
    public PhotonView myPV;

    [Header("Player Hat / Hair")]
    public GameObject[] hairStyle;
    public int hairIndex;

    [Header("Player Chest")]
    public GameObject[] chestStyle;
    public int chestIndex;

    [Header("Player Legs")]
    public GameObject[] leftLegs;
    public GameObject[] rightLegs;
    public int legsIndex;

    public int CurrentHairIndex
    {
        get { return hairIndex; }
        set
        {
            if (value >= 0 && value < hairStyle.Length)
            {
                hairIndex = value;
                HairUpdater();
            }
            else if (value < 0 || value >= hairStyle.Length)
            {
                hairIndex = 0;
                HairUpdater();
            }
        }
    }
    public int CurrentChestIndex
    {
        get { return chestIndex; }
        set
        {
            if (value >= 0 && value < chestStyle.Length)
            {
                chestIndex = value;
                ChestUpdater();
            }
            else if (value < 0 || value >= chestStyle.Length)
            {
                chestIndex = 0;
                ChestUpdater();
            }
        }
    }

    public int CurrentLegIndex
    {
        get { return legsIndex; }
        set
        {
            if (value >= 0 && value < leftLegs.Length)
            {
                legsIndex = value;
                LegUpdater();
            }
            else if (value < 0 || value >= leftLegs.Length)
            {
                legsIndex = 0;
                LegUpdater();
            }
        }
    }

    public string hairIndexString;
    public string chestIndexString;
    public string legsIndexString;

    // Start is called before the first frame update
    void Start()
    {
        // Player Hat / Hair
        //hairIndex = hairStyle.Length;
        CurrentHairIndex = PlayerData.instance.data.playerHat;
        //for (int i = 0; i < hairStyle.Length; i++)
        //{
        //    hairStyle[i].SetActive(i == hairIndex);
        //}

        // Player Chest
        //chestIndex = chestStyle.Length;
        CurrentChestIndex = PlayerData.instance.data.playerChest;
        //for (int i = 0; i < chestStyle.Length; i++)
        //{
        //    chestStyle[i].SetActive(i == chestIndex);
        //}

        // Player Legs
        //legsIndex = leftLegs.Length;
        CurrentLegIndex = PlayerData.instance.data.playerLegs;
        //for (int i = 0; i < leftLegs.Length; i++)
        //{
        //    leftLegs[i].SetActive(i == legsIndex);
        //    rightLegs[i].SetActive(i == legsIndex);
        //}

        myPV = GetComponent<PhotonView>();

        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            hairIndexString = (string)myPV.Owner.CustomProperties["Hair"];
            chestIndexString = (string)myPV.Owner.CustomProperties["Chest"];
            legsIndexString = (string)myPV.Owner.CustomProperties["Legs"];

            CurrentHairIndex = Int32.Parse(hairIndexString);
            CurrentChestIndex = Int32.Parse(chestIndexString);
            CurrentLegIndex = Int32.Parse(legsIndexString);
        }
    }
    //private void Update()
    //{
    //    CurrentHairIndex = hairIndex;
    //    CurrentChestIndex = chestIndex;
    //    CurrentLegIndex = legsIndex;
    //}

    public void HairUpdater()
    {
        for (int i = 0; i < hairStyle.Length; i++)
        {
            hairStyle[i].SetActive(i == CurrentHairIndex);
        }
        hairIndex = CurrentHairIndex;
    }

    public void ChestUpdater()
    {
        for (int i = 0; i < chestStyle.Length; i++)
        {
            chestStyle[i].SetActive(i == CurrentChestIndex);
        }
        chestIndex = CurrentChestIndex;
    }

    public void LegUpdater()
    {
        for (int i = 0; i < leftLegs.Length; i++)
        {
            leftLegs[i].SetActive(i == CurrentLegIndex);
            rightLegs[i].SetActive(i == CurrentLegIndex);
        }
        legsIndex = CurrentLegIndex;
    }

    public void SetAvatar(Data avatarData)
    {
        //hairStyle[avatarData.playerHat].SetActive(true);
        //chestStyle[avatarData.playerChest].SetActive(true);
        //leftLegs[avatarData.playerLegs].SetActive(true);

        CurrentHairIndex = avatarData.playerHat;
        CurrentChestIndex = avatarData.playerChest;
        CurrentLegIndex = avatarData.playerLegs;

        ////CurrentHairIndex = PlayerPrefs.GetInt("Hair");
        ////CurrentChestIndex = PlayerPrefs.GetInt("Chest");
        ////CurrentLegIndex = PlayerPrefs.GetInt("Legs");

        //HairUpdater();
        //ChestUpdater();
        //LegUpdater();
    }
}