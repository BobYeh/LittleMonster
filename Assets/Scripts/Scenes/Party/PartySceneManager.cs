using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common.Managers;

public class PartySceneManager : SceneManagerBase
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    EditPartyViewManager editPartyViewManager;

    public override void OnOpen()
    {
        base.OnOpen();
        animator.Play("OnOpenPartyScene");
        editPartyViewManager.OnOpen();
    }

    public void OnClickedBackButton()
    {
        SceneManager.Instance.SwitchScene(SceneName.MONSTER);
    }   
}
