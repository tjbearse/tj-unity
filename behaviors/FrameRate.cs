using UnityEngine;

namespace TJ {
	public class FrameRate : MonoBehaviour
	{
		[SerializeField]
		[Range(10, 60)]
		int frameRate = 30;
		void Update()
		{
			Application.targetFrameRate = frameRate;
		}
	}

}
