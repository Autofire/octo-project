using UnityEngine;
using ReachBeyond.VariableObjects;

public class ManualFloatCounter : MonoBehaviour
{
#pragma warning disable CS0649
	[SerializeField] private FloatReference counter;
	[SerializeField] private FloatConstReference delta;
#pragma warning restore CS0649

	public void Tick() {
		counter.Value += delta;
	}

}
