using UnityEngine;

public abstract class InteractionPoint : MonoBehaviour
{
    public abstract string InteractionText { get; }
    public abstract void Execute();
}
