using TMPro;
using Unity.Mathematics;

namespace CodeBase.ECS.Components
{
	public struct SpawnEvent
	{
		public float3 Position;
		public string Path;
	}

	public struct CreateMarkerTMP
	{
		public int Number;
	}
	public struct NumberTMP
	{
		public Text Text;
	}

	public class Text
	{
		public TMP_Text TMPText;

		public void SetText(int number)
		{
			TMPText.text = number.ToString();
		}
	}
}