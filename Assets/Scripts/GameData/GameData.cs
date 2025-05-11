using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security;

[System.Serializable]
public class GameData
{
    //gamedata
    public int coins;
    public SerializeDictionary<string, bool> charactersUnlocked;
    //options
    public bool damageNumbersOn;
    public float MusicVolume;
    public float SFXMusic;
    public GameData()
    {
        this.coins = 0;
        charactersUnlocked = new SerializeDictionary<string, bool>();
        damageNumbersOn = true;
        MusicVolume = 0.5f;
        SFXMusic = 1;
    }
}
