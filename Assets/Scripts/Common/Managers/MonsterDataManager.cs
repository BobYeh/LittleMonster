using UnityEngine;
using System.Collections.Generic;
using Common.Utils;

public class MonsterDataManager :  SingletonMonoBehaviour<MonsterDataManager>
{
    Dictionary<int, MonsterEntity> playerMonsterData = new Dictionary<int, MonsterEntity>();

    public Dictionary<int, MonsterEntity> PlayerMonsterData
    {
        get
        {
            return playerMonsterData;
        }
    }

    public Dictionary<int, MonsterEntity> SetPlayerMonsterData(List<MonsterEntity> data)
    {
        playerMonsterData = new Dictionary<int, MonsterEntity>();

        foreach (var entity in data)
        {
            playerMonsterData.Add(entity.monsterId, entity);
        }

        return playerMonsterData;
    }

    public MonsterEntity GetMonsterData(int monsterId)
    {
        if (playerMonsterData.ContainsKey(monsterId))
            return playerMonsterData[monsterId];
        else
            Debug.Log("Try to get monster data not exist: monsterId: " + monsterId);

        return null;
    }
}
