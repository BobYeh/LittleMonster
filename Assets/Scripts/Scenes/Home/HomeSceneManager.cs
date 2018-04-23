using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField]
    Text playerID;

    private void Awake()
    {
        playerID.text = string.Format("PlayerID: {0}", PlayerDataManager.Instance.GetPlayerId());
    }

    public void OnClickedBackToTitleButton()
    {
        SceneManager.Instance.SwitchScene(SceneName.HOME, SceneName.TITLE);
    }
}
