using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Utils;
using Common.Managers;
using UnityEngine.UI;

public class HomeGroupPagesController : SingletonMonoBehaviour<HomeGroupPagesController>
{
    [SerializeField]
    RadioButtonGroupController radioButtonGroupController;

    string currentSceneName = "";

    private void Awake()
    {
        if (radioButtonGroupController != null)
            radioButtonGroupController.selectedRadioButtonChangedHandler += OnRadioButtonChanged;
    }

    public void OnRadioButtonChanged(string radioButtonKey)
    {
        string nextSceneName = GetSceneNameByKey(radioButtonKey);

        if (currentSceneName != nextSceneName)
        {
            SceneManager.instance.SwitchScene(currentSceneName, nextSceneName);
            currentSceneName = nextSceneName;
        }
    }

    public string GetSceneNameByKey(string key)
    {
        switch(key)
        {
            case "Home":
                return SceneName.HOME;
            case "Battle":
                return SceneName.HOME;
            case "MonsterList":
                return SceneName.MONSTER_LIST_SCENE;
             case "ItemList":
                return SceneName.HOME;
            case "Shop":
                return SceneName.HOME;
            case "Option":
                return SceneName.HOME;
            default:
                return SceneName.HOME;
        }
    }
}
