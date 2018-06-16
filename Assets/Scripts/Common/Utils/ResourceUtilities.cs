using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common.Utils;
using UnityEngine.U2D;

public class ResourceUtilities : SingletonMonoBehaviour<ResourceUtilities>
{
    public void LoadMonsterIcon(int masterId, Action<Sprite> act)
    {
        string atlasPath = "SpriteAtlas/Monster/MonsterIcons";
        string fileName = "Monster_Icon_" + masterId.ToString("D3");

        var request = Resources.LoadAsync<SpriteAtlas>(atlasPath);

        request.completed +=(async)=> {
            if (request.asset != null)
            {
                SpriteAtlas atlas = (SpriteAtlas)request.asset;
                Sprite sprite = atlas.GetSprite(fileName);
                if (sprite != null)
                {
                    act(sprite);
                }
                else
                {
                    Debug.Log("LoadMonsterIcon failed: masterId: " + masterId);
                }
            }
            else
                Debug.Log("LoadAtals failed: path: " + atlasPath);
        };
    }
}
