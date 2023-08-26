using CodeBase.ECS.Components;
using CodeBase.Services.StaticData;
using CodeBase.Services.ViewsFactory;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Player
{
	public class CreateMinionViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;

		public CreateMinionViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Exc<View>().Exc<Dead>().Exc<DeathEvent>().End();

			viewPool = world.GetPool<View>();
			spawnEventPool = world.GetPool<SpawnEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				var alienView = viewsFactory.CreateMinion(spawnEvent.Path);
				ref var view = ref viewPool.Add(entity);
				view.Value = alienView;
				view.Value.UpdatePosition(spawnEvent.Position);
				spawnEventPool.Del(entity);
			}
		}
	}
	public class CreateTmpNumberSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly INumberTMPFactory numberTMPFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<NumberTMP> numberTMPPool;
		private EcsPool<CreateMarkerTMP> createMarkerkPool;

		public CreateTmpNumberSystem(INumberTMPFactory numberTMPFactory)
		{
			this.numberTMPFactory = numberTMPFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Inc<View>().Inc<CreateMarkerTMP>().End();

			viewPool = world.GetPool<View>();
			numberTMPPool = world.GetPool<NumberTMP>();
			createMarkerkPool = world.GetPool<CreateMarkerTMP>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				var view1 = viewPool.Get(entity);
				ref var createMarkerTMP =ref  createMarkerkPool.Get(entity);
				ref var numberTMP = ref numberTMPPool.Add(entity);
				numberTMP.Text = numberTMPFactory.CreateTmp(view1.Value);
				numberTMP.Text.SetText(createMarkerTMP.Number);
				createMarkerkPool.Del(entity);
			}
		}
	}
}