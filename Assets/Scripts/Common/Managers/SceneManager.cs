using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common.Utils;

namespace Common.Managers
{
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        //Key: Scene Name in Const class value:Top object for each scene
        Dictionary<string, SceneManagerBase> activeScenes = new Dictionary<string, SceneManagerBase>();

        public void AddActiveScene(string key, SceneManagerBase sceneManager)
        {
            if (!activeScenes.ContainsKey(key))
            {
                activeScenes.Add(key, sceneManager);
            }
        }

        public void RemoveActiveScene(string key)
        {
            if (activeScenes.ContainsKey(key))
            {
                activeScenes.Remove(key);
            }
        }

        public void SwitchScene(string currentScene, string nextScene)
        {
            if (activeScenes.ContainsKey(currentScene))
            {
                activeScenes[currentScene].OnClose();
            }

            if (activeScenes.ContainsKey(nextScene))
            {
                activeScenes[nextScene].OnOpen();
            }
            else
            {
                Hover(nextScene);
            }
        }

        public void Replace(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }

        public void Hover(string name)
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(name).isLoaded)
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            }
        }

        public void Dispose(string name)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(name).isLoaded)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(name);
            }
        }
    }
}
