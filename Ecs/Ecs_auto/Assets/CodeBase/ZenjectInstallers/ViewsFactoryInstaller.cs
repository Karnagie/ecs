using CodeBase.MonoBehaviourView;
using CodeBase.Services.ViewsFactory;
using Zenject;

namespace CodeBase.ZenjectInstallers
{
	public class ViewsFactoryInstaller : Installer<ViewsFactoryInstaller>
	{
		public override void InstallBindings()
		{
			AlienViewFactory();
			PlayerViewFactory();
			BulletViewFactory();
			LootViewFactory();
			
			BindViewsFactory();
		}

		private void BindViewsFactory() => 
			Container.BindInterfacesTo<ViewsFactory>().AsSingle();

		private void AlienViewFactory()
		{
			Container
				.BindFactory<string, AlienView, AlienView.Factory>()
				.FromFactory<PrefabResourceFactory<AlienView>>();
		}
		private void PlayerViewFactory()
		{
			Container
				.BindFactory<string, MinionView, MinionView.Factory>()
				.FromFactory<PrefabResourceFactory<MinionView>>();
		}
		private void BulletViewFactory()
		{
			Container
				.BindFactory<string, BulletView, BulletView.Factory>()
				.FromFactory<PrefabResourceFactory<BulletView>>();
		}
		private void LootViewFactory()
		{
			Container
				.BindFactory<string, LootView, LootView.Factory>()
				.FromFactory<PrefabResourceFactory<LootView>>();
		}
	}
}