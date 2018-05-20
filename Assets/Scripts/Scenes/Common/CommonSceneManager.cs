using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Managers;

public class CommonSceneManager : SceneManagerBase
{
    protected override void Awake()
    {
        StartCoroutine(OpenTitleScene());
    }

    IEnumerator OpenTitleScene()
    {
        if (!SceneManager.Instance.IsGroupLoaded(SceneGroup.TitleGroup))
            yield return SceneManager.Instance.LoadSceneGroup(SceneGroup.TitleGroup);

        SceneManager.Instance.OpenScene(SceneName.TITLE);
    }
}
