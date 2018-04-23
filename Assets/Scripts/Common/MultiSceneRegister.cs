using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Managers;

public class MultiSceneRegister : MonoBehaviour
{
    [SerializeField]
    string key;
    [SerializeField]
    SceneManagerBase sceneManager;

    private void Awake()
    {
        SceneManager.Instance.AddActiveScene(key, sceneManager);
    }
}
