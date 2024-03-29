using CodeBase.Data;
using CodeBase.Infrastructure.CoreEngine;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class LoadLevelState : IPaylodedState<string>
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;
		private readonly ISceneLoader sceneLoader;
		private readonly ICoreEngine coreEngine;

		public LoadLevelState(IGameStateMachine gameStateMachine,
			IStaticDataService staticDataService,
			ISceneLoader sceneLoader,
			ICoreEngine coreEngine)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
			this.sceneLoader = sceneLoader;
			this.coreEngine = coreEngine;
		}

		public void Enter(string sceneName)
		{
			OnLoaded();
			//sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit()
		{
		}

		private void OnLoaded()
		{
			var levelConfig = staticDataService.ForLevelTemplate(1);

			InitSession(levelConfig);
			PreWarmCore();

			gameStateMachine.Enter<GameLoopState>();
		}

		private void InitSession(LevelData levelConfig) =>
			coreEngine.InitSession(levelConfig);

		private void PreWarmCore() =>
			coreEngine.Tick();


		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}
}