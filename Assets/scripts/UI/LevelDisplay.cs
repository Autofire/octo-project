using UnityEngine;
using UnityEngine.UI;
using ReachBeyond.VariableObjects;

public class LevelDisplay : MonoBehaviour
{
	public Text text;
	public IntConstReference lvlNum;

	private void OnGUI() {
		text.text = "Level: " + lvlNum;
	}
}
