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
        SceneManager.Instance.SwitchScene(SceneName.TITLE, SceneName.HOME);
    }

    IEnumerator RegisterAccount()
    {
        yield return http.Post(API.RegisterAccount(), (request) =>
        {
            playerId = Convert.ToInt32(request.downloadHandler.text);
            StartCoroutine(TryLogin(playerId));
        });
    }

    IEnumerator TryLogin(int playerId)
    {
        yield return http.Post(API.Login(playerId), (request)=>
        {
            if (request.downloadHandler.text == "Login Success")
            {
                PlayerDataManager.Instance.SavePlayerId(playerId);
                SwitchToHomeScene();
            }
        });
    }
}
