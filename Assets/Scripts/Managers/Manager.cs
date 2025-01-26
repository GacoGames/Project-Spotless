using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    // Asynchronous initialization
    public virtual async UniTask InitializeAsync()
    {
        Debug.Log($"[{name}] Initialization started.");
        await UniTask.Yield(); // Perform any async initialization here
        Debug.Log($"[{name}] Initialization complete.");
    }

    // Cleanup method
    public virtual void Cleanup()
    {
        Debug.Log($"[{name}] Cleanup called.");
    }
}
