using UnityEngine;
using ReachBeyond.VariableObjects;

public class RandomSpawner : MonoBehaviour {
	public GameObject[] spawnPrefabs;
	public FloatConstReference spawnChance;
	public FloatConstReference spawnChanceDividend;

	public void Start() {
		if(spawnPrefabs.Length > 0 && Random.Range(0f, 1f) <= spawnChance / spawnChanceDividend) {
			GameObject chosenPrefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

			Instantiate(chosenPrefab, transform.position, chosenPrefab.transform.rotation, transform.parent);
		}

		Destroy(gameObject);
	}
}
