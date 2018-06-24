using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API
{
    static string host = "http://183.181.20.96:8081/";
    public static string SET_COOKIE = "Set-Cookie";
    public static string COOKIE = "Cookie";

    public static UnityWebRequest Login(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        var uri = host + APIName.LOGIN;

        return UnityWebRequest.Post(uri, form);
    }

    public static UnityWebRequest IsLogin()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.IS_LOGIN;

        var request = UnityWebRequest.Get(uri);
        request.SetRequestHeader(COOKIE, PlayerPrefs.GetString(COOKIE));

        return request;
    }

    public static UnityWebRequest RegisterAccount()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.REGISTER_ACCOUNT;

        return UnityWebRequest.Post(uri, form);
    }

    public static UnityWebRequest GetPlayerMonsters()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.GET_PLAYER_MONSTERS;

        var request = UnityWebRequest.Post(uri, form);
        request.SetRequestHeader(COOKIE, PlayerPrefs.GetString(COOKIE));

        return request;
    }

    public static UnityWebRequest GetPlayerParties()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.GET_PLAYER_PARTIES;

        var request = UnityWebRequest.Post(uri, form);
        request.SetRequestHeader(COOKIE, PlayerPrefs.GetString(COOKIE));

        return request;
    }

    public static UnityWebRequest UpdatePlayerParty(PartyEntity partyData)
    {
        WWWForm form = new WWWForm();
        form.AddField("partyNumber", partyData.PartyNumber);

        for (int i = 0; i < GameDefineData.NUMBER_OF_PARTY_MEMBER; i++)
        {
            if (partyData.MemberMonsterIds[i] > 0)
            {
                form.AddField(string.Format("position{0}MonsterId", i), partyData.MemberMonsterIds[i]);
                Debug.Log(string.Format("position{0}MonsterId: {1}", i, partyData.MemberMonsterIds[i]));
            }
        }

        Debug.Log(string.Format("UpdatePlayerParty PartyNumber{0}", partyData.PartyNumber));

        var uri = host + APIName.UPDATE_PLAYER_PARTY;

        var request = UnityWebRequest.Post(uri, form);
        request.SetRequestHeader(COOKIE, PlayerPrefs.GetString(COOKIE));

        return request;
    }
}
