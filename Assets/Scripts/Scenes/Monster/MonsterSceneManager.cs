using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;
using MiniJSON;
using System;
using System.Reflection;
using System.Linq;

public class MonsterSceneManager : SceneManagerBase
{
    [SerializeField]
    HttpPostTest http;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GetPlayerMonsters());
    }

    IEnumerator IsLogin()
    {
        yield return http.Post(API.IsLogin(), (request) =>
        {
            Debug.Log(request.GetRequestHeader(API.COOKIE));
        });
    }

    IEnumerator GetPlayerMonsters()
    {
        yield return http.Post(API.GetPlayerMonsters(), (request) =>
        {
            Debug.Log("GetPlayerMonsters: " + request.downloadHandler.text);
            var monsterData = JsonHelpers.GetObjectList<MonsterEntity>(request.downloadHandler.text);
            MonsterDataManager.Instance.SetPlayerMonsterData(monsterData);
        });
    }

    #region Button Event

    public void OnClickedPartyButton()
    {
        SceneManager.Instance.SwitchScene(SceneName.PARTY);
    }

    #endregion
}

