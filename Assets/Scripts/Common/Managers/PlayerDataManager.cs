using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Utils;

public class PlayerDataManager :  SingletonMonoBehaviour<PlayerDataManager>
{
    public int GetPlayerId()
    {
        return PlayerPrefs.GetInt(GameKey.PLAYER_ID);
    }

    public void SavePlayerId(int id)
    {
        PlayerPrefs.SetInt(GameKey.PLAYER_ID, id);
    }

    public bool FirstTimeLogin()
    {
        return !PlayerPrefs.HasKey(GameKey.PLAYER_ID);
    }
}
