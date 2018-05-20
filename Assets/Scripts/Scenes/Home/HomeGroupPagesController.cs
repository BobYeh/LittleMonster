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
                return SceneName.BATTLE_MAP;
            case "MonsterList":
                return SceneName.MONSTER_LIST;
             case "ItemList":
                return SceneName.ITEM_LIST;
            case "Shop":
                return SceneName.SHOP;
            case "Option":
                return SceneName.OPTION;
            default:
                return SceneName.HOME;
        }
    }
}
