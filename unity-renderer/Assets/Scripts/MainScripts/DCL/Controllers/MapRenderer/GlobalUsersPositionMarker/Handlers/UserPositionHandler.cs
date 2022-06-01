using System;
using UnityEngine;

/// <summary>
/// Handle user position changes to let other handler or controller to react appropriately
/// </summary>
internal class UserPositionHandler : IDisposable
{
    public Vector2Int playerCoords { private set; get; }
    public event Action<Vector2Int> OnPlayerCoordsChanged;

    public UserPositionHandler()
    {
        playerCoords = ABEYController.i.CommonScriptables.playerCoords.Get();
        ABEYController.i.CommonScriptables.playerCoords.OnChange += OnPlayerCoords;
    }

    public void Dispose() { ABEYController.i.CommonScriptables.playerCoords.OnChange -= OnPlayerCoords; }

    private void OnPlayerCoords(Vector2Int current, Vector2Int prev)
    {
        playerCoords = current;
        OnPlayerCoordsChanged?.Invoke(playerCoords);
    }
}