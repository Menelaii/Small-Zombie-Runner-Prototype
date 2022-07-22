
using UnityEngine;

public interface IAttentionThing
{
    AttentionPriority Priority { get; }
    Transform Transform { get; }
}
