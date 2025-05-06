using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security;
public class GameData
{
    public int coins;
    public SerializeDictionary<string, bool> charactersUnlocked;
    public GameData()
    {
        this.coins = 0;
        charactersUnlocked = new SerializeDictionary<string, bool>();
    }
}
