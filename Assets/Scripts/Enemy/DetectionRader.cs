using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRader : MonoBehaviour
{
    public List<Player> targets;

    private void Awake()
    {
        targets = new List<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            targets.Add(player);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            targets.Remove(player);
    }
}
