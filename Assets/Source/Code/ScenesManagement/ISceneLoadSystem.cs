using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Source.Code.ScenesManagement
{
    public interface ISceneLoadSystem
    {
        UniTask LoadFromAddressables(AssetReference sceneReference, LoadSceneMode loadSceneMode, bool activateOnLoad);

        UniTask<AsyncOperation> LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single,
            bool allowSceneActivation = true);
    }
}