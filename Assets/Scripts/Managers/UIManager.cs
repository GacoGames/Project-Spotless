using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

// handles the initialization of UI prefabs

public class UIManager : Manager
{
    public List<AssetReferenceGameObject> uiPrefabs;

    public override async UniTask InitializeAsync()
    {
        Debug.Log($"[{name}] Initialization started.");

        foreach (var uiPrefab in uiPrefabs)
        {
            if (uiPrefab == null) continue;

            var handle = Addressables.LoadAssetAsync<GameObject>(uiPrefab);
            var loadedPrefab = await handle.Task; //wait for the load to complete
            Instantiate(loadedPrefab, transform);
        }

        Debug.Log($"[{name}] Initialization complete.");
    }
    public override void Cleanup()
    {
        base.Cleanup();
    }
}