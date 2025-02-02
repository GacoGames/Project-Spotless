using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string Title => gameObject.name;
    public Collider2D roomBoundary;
    public List<GameplayObject> interactableObjects = new List<GameplayObject>();
}
