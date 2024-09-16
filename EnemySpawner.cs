using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
	public float cooldown;
	public float waveCooldown;
	public TextMeshProUGUI waveText;
	private int waveCount;

	private void Start()
	{
		waveCount = 1;
		StartCoroutine("SpawnCoroutine");
	}

	public IEnumerator SpawnCoroutine()
	{
		while (true)
		{
			//waveText.text = "Wave: " + waveCount;
			int enemyCount = Random.Range(waveCount, (int)Mathf.Pow(waveCount, 1.5f) + 1);
			for (int i = 0; i < enemyCount; i++)
			{
				Instantiate(enemy, this.transform.position, Quaternion.identity);
				yield return new WaitForSeconds(cooldown);
			}
			waveCount += 1;
			yield return new WaitForSeconds(waveCooldown);
		}
	}
}
