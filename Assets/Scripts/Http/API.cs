using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API
{
    static string host = "http://183.181.20.96:8081/";

    public static UnityWebRequest Login(int id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        var uri = host + APIName.LOGIN;

        return UnityWebRequest.Post(uri, form);
    }

    public static UnityWebRequest RegisterAccount()
    {
        WWWForm form = new WWWForm();

        var uri = host + APIName.REGISTER_ACCOUNT;

        return UnityWebRequest.Post(uri, form);
    }
}
