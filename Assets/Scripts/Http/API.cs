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

    public static UnityWebRequest UpdatePlayerParty()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.UPDATE_PLAYER_PARTY;

        var request = UnityWebRequest.Post(uri, form);
        request.SetRequestHeader(COOKIE, PlayerPrefs.GetString(COOKIE));

        return request;
    }
}
