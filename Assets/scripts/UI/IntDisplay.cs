using UnityEngine;
using UnityEngine.UI;
using ReachBeyond.VariableObjects;

public class IntDisplay : MonoBehaviour
{
	public Text text;
	public string prefix;
	public IntConstReference value;

	private void OnGUI() {
		text.text = prefix + value;
	}
}
