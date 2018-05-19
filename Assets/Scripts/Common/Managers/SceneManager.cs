using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Common.Utils;
using System;

namespace Common.Managers
{
    public enum SceneGroup
    {
        TitleGroup,
        HomeGroup,
    }

    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        //Key: Scene Name in Const class value:Top object for each scene
        Dictionary<string, SceneManagerBase> activeScenes = new Dictionary<string, SceneManagerBase>();
        Dictionary<AsyncOperation, string> loadingScenes = new Dictionary<AsyncOperation, string>();
        List<SceneGroup> loadedGroup = new List<SceneGroup>();

        int numberOfScenesNeedToBeLoad;
        int numberOfLoadedScene;
        

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

        public bool IsGroupLoaded(SceneGroup group)
        {
            return loadedGroup.Contains(group);
        }

        public IEnumerator LoadSceneGroup(SceneGroup group)
        {
            string[] sceneNames = null;

            switch (group)
            {
                case SceneGroup.HomeGroup:
                    sceneNames = SceneName.HOME_GROUP;
                    break;
                case SceneGroup.TitleGroup:
                    sceneNames = SceneName.TITLE_GROUP;
                    break;
                default:
                    break;
            }

            numberOfLoadedScene = 0;
            numberOfScenesNeedToBeLoad = sceneNames.Length;
            loadingScenes = new Dictionary<AsyncOperation, string>();

            foreach (var name in sceneNames)
            {
                var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                loadingScenes.Add(asyncOperation, name);
                asyncOperation.completed += GroupSceneLoaded;
            }

            while (numberOfLoadedScene != numberOfScenesNeedToBeLoad)
            {
                yield return null;
            }

            loadedGroup.Add(group);
        }

        void GroupSceneLoaded(AsyncOperation asyncOperation)
        {
            numberOfLoadedScene += 1;
        }
    }
}
