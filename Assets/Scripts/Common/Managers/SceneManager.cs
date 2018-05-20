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

        int numberOfScenesToBeHandle;
        int numberOfHandledScene;


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

        public void OpenScene(string sceneName)
        {
            if (activeScenes.ContainsKey(sceneName))
            {
                activeScenes[sceneName].OnOpen();
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
                ;
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

        #region Handle Scene Group

        public IEnumerator SwitchSceneGroup(SceneGroup currentGroup, SceneGroup nextGroup, string openSceneName)
        {
            if (!loadedGroup.Contains(nextGroup))
            {
                yield return LoadSceneGroup(nextGroup);
            }

            OpenScene(openSceneName);

            if (loadedGroup.Contains(currentGroup))
            {
                yield return DisposeSceneGroup(currentGroup);
            }
        }

        public IEnumerator LoadSceneGroup(SceneGroup group)
        {
            string[] sceneNames = GetSceneNames(group);

            numberOfHandledScene = 0;
            numberOfScenesToBeHandle = sceneNames.Length;
            loadingScenes = new Dictionary<AsyncOperation, string>();

            loadedGroup.Add(group);

            foreach (var name in sceneNames)
            {
                var asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                loadingScenes.Add(asyncOperation, name);
                asyncOperation.completed += SceneHandleFinished;
            }

            while (numberOfHandledScene != numberOfScenesToBeHandle)
            {
                yield return null;
            }
        }

        public IEnumerator DisposeSceneGroup(SceneGroup group)
        {
            string[] sceneNames = GetSceneNames(group);

            numberOfHandledScene = 0;
            numberOfScenesToBeHandle = sceneNames.Length;
            loadingScenes = new Dictionary<AsyncOperation, string>();

            loadedGroup.Remove(group);

            foreach (var name in sceneNames)
            {
                var asyncOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(name);
                loadingScenes.Add(asyncOperation, name);
                asyncOperation.completed += SceneHandleFinished;

                if (activeScenes.ContainsKey(name))
                    activeScenes.Remove(name);
            }

            while (numberOfHandledScene != numberOfScenesToBeHandle)
            {
                yield return null;
            }
        }

        public string[] GetSceneNames(SceneGroup group)
        {
            switch (group)
            {
                case SceneGroup.HomeGroup:
                    return SceneName.HOME_GROUP;
                case SceneGroup.TitleGroup:
                    return SceneName.TITLE_GROUP;
                default:
                    return null;
            }
        }

        void SceneHandleFinished(AsyncOperation asyncOperation)
        {
            numberOfHandledScene += 1;
        }

        #endregion
    }
}
