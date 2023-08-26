using CodeBase.Configs;
using CodeBase.Data;
using CodeBase.ECS.Components;
using CodeBase.ECS.Factories;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Create
{
	public class CreateRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter requestsFilter;
		private EcsPool<RoundCreateRequest> requestPool;
		private EcsPool<Used> usedPool;

		public CreateRoundSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			requestsFilter = world.Filter<RoundCreateRequest>().Exc<Used>().End();

			requestPool = world.GetPool<RoundCreateRequest>();
			usedPool = world.GetPool<Used>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in requestsFilter)
			{
				ref var levelCreateRequest = ref requestPool.Get(entity);
				var levelConfig = levelCreateRequest.Config;

				for (var index = 0; index < levelConfig.PlayerSpawnPoints.Count; index++)
				{
					var player = levelConfig.PlayerSpawnPoints[index];
					CreatePlayer(player,index);
				}

				usedPool.Add(entity);
			}
		}

		private void CreatePlayer(PlayerSpawnPoint spawnPoint, int index)
		{
			var data = staticDataService.ForPlayer(spawnPoint.PlayerType);
			data.Position = spawnPoint.PlayerInitialPoint;
			PlayerFactory.Create(world, data,index);
		}
	}
}