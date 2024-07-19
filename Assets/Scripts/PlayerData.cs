using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public CharacterCustomizer characterCustomizer;
    public Data data;

    public void OnEnable()
    {
        data = new Data();
        PlayerData.instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string AvatarToString()
    {
        string returnString = JsonUtility.ToJson(PlayerData.instance.data);
        return returnString;
    }

    public void SetCostume()
    {
        PlayerData.instance.data.playerHat = characterCustomizer.hairIndexData;
        PlayerData.instance.data.playerChest = characterCustomizer.chestIndexData;
        PlayerData.instance.data.playerLegs = characterCustomizer.legsIndexData;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[Serializable]
public class Data
{
    public static PlayerData instance;
    public int playerHat;
    public int playerChest;
    public int playerLegs;
}

