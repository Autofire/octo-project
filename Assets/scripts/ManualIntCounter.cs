using UnityEngine;
using ReachBeyond.VariableObjects;

public class ManualIntCounter : MonoBehaviour
{
#pragma warning disable CS0649
	[SerializeField] private IntReference counter;
	[SerializeField] private IntConstReference delta;
#pragma warning restore CS0649

	public void Tick() {
		counter.Value += delta;
	}

}
