using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Managers;

public class CommonSceneManager : SceneManagerBase
{
    private void Awake()
    {
        SceneManager.Instance.Hover(SceneName.TITLE);
    }
}
