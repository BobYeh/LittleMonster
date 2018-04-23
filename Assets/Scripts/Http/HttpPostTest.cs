using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpPostTest : MonoBehaviour
{
    public IEnumerator Post(UnityWebRequest request, Action<UnityWebRequest> action)
    {
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete");
            action(request);
        }
    }
}
