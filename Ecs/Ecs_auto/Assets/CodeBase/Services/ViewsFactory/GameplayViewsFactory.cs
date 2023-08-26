using CodeBase.ECS.Components;
using CodeBase.MonoBehaviourView;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.ViewsFactory
{
	public interface INumberTMPFactory
	{
		Text CreateTmp(IUnityObjectView view);
	}
	
	public class NumberTMPFactory:INumberTMPFactory
	{
		
		public Text CreateTmp(IUnityObjectView view)
		{
			var unityObjectView = view as MonoBehaviour;
			var tmpText = unityObjectView.gameObject.GetComponentInChildren<TextMeshProUGUI>();
			var text = new Text();
			text.TMPText = tmpText;
			return text;
		}
	}
	

	public interface IViewsFactory
	{
		IUnityObjectView CreateAlien(string path);
		IUnityObjectView CreateMinion(string spawnEventPath);
		IUnityObjectView CreateBullet(string spawnEventPath);
		IUnityObjectView CreateLoot(string spawnEventPath);
	}

	public class ViewsFactory : IViewsFactory
	{
		private readonly AlienView.Factory alienFactory;
		private readonly MinionView.Factory minionFactory;
		private readonly BulletView.Factory bulletFactory;
		private readonly LootView.Factory lootFactory;

		public ViewsFactory(AlienView.Factory alienFactory, MinionView.Factory minionFactory,
			BulletView.Factory bulletFactory, LootView.Factory lootFactory)
		{
			this.alienFactory = alienFactory;
			this.minionFactory = minionFactory;
			this.bulletFactory = bulletFactory;
			this.lootFactory = lootFactory;
		}

		public IUnityObjectView CreateAlien(string path)
		{
			var view = alienFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}

		public IUnityObjectView CreateMinion(string path)
		{
			var view = minionFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}

		public IUnityObjectView CreateBullet(string path)
		{
			var view = bulletFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}

		public IUnityObjectView CreateLoot(string path)
		{
			var view = lootFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}
	}
}