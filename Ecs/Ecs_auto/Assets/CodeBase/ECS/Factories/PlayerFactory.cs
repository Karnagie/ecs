using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.ECS.Components;
using CodeBase.Enums;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class PlayerFactory
	{
		public static int Create(EcsWorld world, PlayerData config, int index)
		{
			var entity = world.NewEntity();

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			var playerPool = world.GetPool<Player>();
			ref var alien = ref playerPool.Add(entity);
			var speedPool = world.GetPool<Speed>();
			ref var speed = ref speedPool.Add(entity);
			speed.Value = config.Speed;


			var markerPool = world.GetPool<CreateMarkerTMP>();
			ref var markerTMP = ref markerPool.Add(entity);
			markerTMP.Number = index;
			
			return entity;
		}
	}
}