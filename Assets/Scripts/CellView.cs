using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public SurfaceType surfaceType;
    public EcsEntity entity;

    public IEnumerator Start()
    {
        yield return null;
        entity = Service<EcsWorld>.Get().NewEntity();
        entity.Get<Cell>().value = this;
    }
}

public struct Cell
{
    public CellView value;
}
