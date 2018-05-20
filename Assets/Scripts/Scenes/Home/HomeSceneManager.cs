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
        StartCoroutine(SceneManager.Instance.SwitchSceneGroup(SceneGroup.HomeGroup, SceneGroup.TitleGroup, SceneName.TITLE));
    }

    public void UpdatePlayerID()
    {
        playerID.text = string.Format("PlayerID: {0}", PlayerDataManager.Instance.GetPlayerId());
    }
}
