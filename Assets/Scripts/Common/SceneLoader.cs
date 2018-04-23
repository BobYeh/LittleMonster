using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Managers;

namespace Common.Utils
{
    public class SceneLoader :  SingletonMonoBehaviour<SceneLoader>
    {
        //Key: Scene Name in Const class value:Top object for each scene
        Dictionary<string, GameObject> activeScenes = new Dictionary<string, GameObject>();

        public void AddActiveScene(string key, GameObject topCanvas)
        {
            if (!activeScenes.ContainsKey(key))
            {
                activeScenes.Add(key, topCanvas);
            }
        }

        public void RemoveActiveScene(string key)
        {
            if (activeScenes.ContainsKey(key))
            {
                activeScenes.Remove(key);
            }
        }

        public void LoadSceneGroup(string[] group)
        {
            foreach (var sceneName in group)
            {
                SceneManager.Instance.Hover(sceneName);
            }
        }
    }
}
