using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;

public class HomeSceneManager : SceneManagerBase
{
    [SerializeField]
    Text playerID;

    protected override void Awake()
    {
        base.Awake();
        UpdatePlayerID();
    }

    public override void OnOpen()
    {
        base.OnOpen();
        UpdatePlayerID();
    }

    public void OnClickedBackToTitleButton()
    {
        SceneManager.Instance.SwitchScene(SceneName.HOME, SceneName.TITLE);
    }

    public void UpdatePlayerID()
    {
        playerID.text = string.Format("PlayerID: {0}", PlayerDataManager.Instance.GetPlayerId());
    }
}
