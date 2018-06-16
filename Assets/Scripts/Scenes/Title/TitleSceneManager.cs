using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;

public class TitleSceneManager : SceneManagerBase
{
    [SerializeField]
    Text Id;
    [SerializeField]
    HttpPostTest http;

    int playerId;

    public void OnClickedTitleScene()
    {
        if (Id.text != "")
        {
            try
            {
                playerId = Convert.ToInt32(Id.text);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }
        }
        else if (PlayerDataManager.Instance.FirstTimeLogin())
        {
            StartCoroutine(RegisterAccount());
        }
        else
        {
            playerId = PlayerDataManager.Instance.GetPlayerId();
        }

        StartCoroutine(TryLogin(playerId));
    }

    public void OnClickedRegisterButton()
    {
        StartCoroutine(RegisterAccount());
    }

    public void SwitchToHomeScene()
    {
        StartCoroutine(SceneManager.Instance.SwitchSceneGroup(SceneGroup.TitleGroup, SceneGroup.HomeGroup, SceneName.HOME));
    }

    IEnumerator RegisterAccount()
    {
        yield return http.Post(API.RegisterAccount(), (request) =>
        {
            Debug.Log(request.downloadHandler.text);
            var userData = JsonUtility.FromJson<UserEntity>(request.downloadHandler.text);
            playerId = userData.playerId;
            StartCoroutine(TryLogin(playerId));
            request.Dispose();
        });
    }

    IEnumerator TryLogin(int playerId)
    {
        yield return http.Post(API.Login(playerId), (request)=>
        {
            if (request.downloadHandler.text == "Login Success")
            {
                Debug.Log(request.GetResponseHeader(API.SET_COOKIE));
                string session = request.GetResponseHeader(API.SET_COOKIE);
                session = session.Substring(0, session.IndexOf(";")+1);
                PlayerPrefs.SetString(API.COOKIE, session);
                PlayerDataManager.Instance.SavePlayerId(playerId);
                SwitchToHomeScene();
                request.Dispose();
            }
        });
    }
}
