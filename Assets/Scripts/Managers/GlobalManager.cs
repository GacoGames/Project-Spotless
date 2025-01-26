using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    [Tooltip("List of manager prefabs to load and initialize in order")]
    public List<GameObject> managerPrefabs;

    private List<Manager> instantiatedManagers = new List<Manager>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private async UniTaskVoid Start()
    {
        await InitializeManagersAsync();
        Debug.Log("All managers are initialized!");
    }

    private async UniTask InitializeManagersAsync()
    {
        foreach (var prefab in managerPrefabs)
        {
            if (prefab == null) continue;

            // Instantiate the prefab
            var managerInstance = Instantiate(prefab, transform);
            var managerComponent = managerInstance.GetComponent<Manager>();

            if (managerComponent != null)
            {
                instantiatedManagers.Add(managerComponent);

                // Call async initialization
                await managerComponent.InitializeAsync();
            }
            else
            {
                Debug.LogError($"Prefab {prefab.name} does not have a Manager component!");
            }
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var manager in instantiatedManagers)
        {
            manager.Cleanup();
        }
    }
}
