using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Source.Code.ScenesManagement
{
    public class SceneLoadingSystem : ISceneLoadSystem
    {
        public async UniTask LoadFromAddressables(AssetReference sceneReference, LoadSceneMode loadSceneMode,
            bool activateOnLoad)
        {
            var asyncOperationHandle = Addressables.LoadSceneAsync(sceneReference, loadSceneMode, activateOnLoad);

            while (asyncOperationHandle.IsDone == false)
            {
                await UniTask.Yield();
                Debug.Log($"progress: {asyncOperationHandle.PercentComplete}");
            }

            asyncOperationHandle.Result.ActivateAsync();
        }

        public async UniTask<AsyncOperation> LoadSceneAsync(string sceneName,
            LoadSceneMode loadSceneMode = LoadSceneMode.Single,
            bool allowSceneActivation = true)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                Debug.LogError($"Already on scene: {sceneName}");
                return null;
            }

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode) ??
                                                throw new ArgumentException();

            loadSceneOperation.allowSceneActivation = allowSceneActivation;

            var targetProgress = allowSceneActivation ? 1f : 0.9f;

            while (Mathf.Approximately(loadSceneOperation.progress, targetProgress) == false)
                await UniTask.Yield();

            Debug.Log($"Scene <{sceneName}> loaded!");

            return loadSceneOperation;
        }
    }
}