using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class PlayerControlSystem : IEcsRunSystem
    {
        EcsFilter<Player> filter;

        StaticData staticData;
        public void Run()
        {
            foreach (var i in filter)
            {
                var player = filter.Get1(i);
                Vector3 direction = default;
                direction.x = Input.GetAxisRaw("Horizontal");
                direction.y = Input.GetAxisRaw("Vertical");
                direction.Normalize();
                Vector3 nextPosition = player.value.transform.position + direction * staticData.playerSpeed * Time.deltaTime;
                var collider = Physics2D.OverlapPoint(nextPosition);
                if (collider && collider.TryGetComponent<CellView>(out var cellView))
                {
                    if (cellView.surfaceType.isWalkable)
                    {
                        player.value.transform.position = nextPosition;
                    }
                    else
                    {
                        var rayCast = Physics2D.Raycast(player.value.transform.position, nextPosition - player.value.transform.position, 0.1f);
                        if (rayCast.collider)
                        {
                            player.value.transform.position = (Vector3)rayCast.point - direction * 0.001f;
                        }
                    }
                }
                else
                {
                    var castDirection = player.value.transform.position - nextPosition;
                    castDirection.Normalize();
                    var rayCast = Physics2D.Raycast(nextPosition, castDirection, 0.1f);

                    if (rayCast.collider)
                    {
                        player.value.transform.position = (Vector3)rayCast.point + castDirection * 0.001f;
                    }
                }
            }
        }
    }
}