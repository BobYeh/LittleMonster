using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;
using MiniJSON;
using System;
using System.Reflection;
using System.Linq;
using MessagePack;

public class MonsterSceneManager : SceneManagerBase
{
    [SerializeField]
    HttpPostTest http;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(GetPlayerMonsters());
        StartCoroutine(GetPartyData());
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
            //var monsterData = JsonHelpers.GetObjectList<MonsterEntity>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.data.Length);
            var monsterData = MessagePackSerializer.Deserialize<List<MonsterEntity>>(request.downloadHandler.data);
            MonsterDataManager.Instance.SetPlayerMonsterData(monsterData);
        });
    }

    IEnumerator GetPartyData()
    {
        yield return http.Post(API.GetPlayerParties(), (request) =>
        { 
            Debug.Log("GetPlayerParties: " + request.downloadHandler.text);
            List<object> datas = Json.Deserialize(request.downloadHandler.text) as List<object>;
            List<PartyData> partyDatas = new List<PartyData>();

            foreach(var data in datas)
            {
                var dic = data as Dictionary<string, object>;
                Debug.Log(dic["party_number"].ToString());
                partyDatas.Add(new PartyData(Convert.ToInt32(dic["party_number"]), dic["party"] as List<object>));
            }

            PartyDataManager.Instance.UpdateDataFromServer(partyDatas);
        });
    }

    #region Button Event

    public void OnClickedPartyButton()
    {
        SceneManager.Instance.SwitchScene(SceneName.PARTY);
    }

    #endregion
}

