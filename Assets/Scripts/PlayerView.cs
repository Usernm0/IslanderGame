using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public EcsEntity entity;

    public IEnumerator Start()
    {
        yield return null;
        entity = Service<EcsWorld>.Get().NewEntity();
        entity.Get<Player>().value = this;
    }
}

public struct Player
{
    public PlayerView value;
}
