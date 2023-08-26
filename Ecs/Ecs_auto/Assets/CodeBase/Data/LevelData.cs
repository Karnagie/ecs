using System;
using System.Collections.Generic;
using CodeBase.Configs;

namespace CodeBase.Data
{
	[Serializable]
	public class LevelData
	{
		public List<PlayerSpawnPoint> PlayerSpawnPoints;
		public List<AlienSpawnPoint> aliens;
	}
}