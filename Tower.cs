using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public TowerStats towerStats;
	public List<GameObject> enemiesInRange = new List<GameObject>();            // En liste der skal indeholde fjender som tårnet kan skyde på
	float remainingCooldown;                                                    // Tiden før næste skud
	public Transform shootOrigin;                                               // Stedet projektilerne skal spawne

	void Start()
	{

	}

	void Update()
	{
		enemiesInRange.RemoveAll(enemy => enemy == null);                       // Ryd op i listen og fjern ødelagte fjender (null-referencer)

		remainingCooldown = remainingCooldown - Time.deltaTime;                 // remaining cooldown får trukket lige så meget fra sig som den tid sidste frame varede

		if (remainingCooldown <= 0)                                             // er tiden gået?
		{
			if (enemiesInRange.Count > 0)                                       // er der fjender som tårnet må skyde på?
			{
				Shoot();                                                        // skyd
			}
		}
		if(enemiesInRange.Count == 0)
        {
			return;
        }
		Vector2 dir = new Vector2(enemiesInRange[0].transform.position.x - transform.position.x, enemiesInRange[0].transform.position.z - transform.position.z);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion target = Quaternion.Euler(0f, -angle + 90f, 0f);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 200f);
		//transform.rotation = Quaternion.Euler(0f, -angle + 90f, 0f);
	}

	private void OnTriggerEnter(Collider other)						// Når noget rør tower's trigger
	{
		if (other.gameObject.name == "Enemy(Clone)")                // Hedder objektet der rør "Enemy(Clone)"?
		{
			enemiesInRange.Add(other.gameObject);                   // Tilføj til listen enemiesInRange
		}
	}

	private void OnTriggerExit(Collider other)							// Når noget forsvinder fra tower's trigger
	{
		if (other.gameObject.name == "Enemy(Clone)")					// Hedder objektet der rør "Enemy(Clone)"?
		{
			enemiesInRange.Remove(other.gameObject);					// Fjern fra listen enemiesInRange
		}
	}

	void Shoot()
	{
		
		if (enemiesInRange.Count > 0)																		// Sørg for at der er en fjende at skyde på
		{
			GameObject targetEnemy = enemiesInRange[0];														// Find den første fjende i listen
			Vector3 direction = (targetEnemy.transform.position - shootOrigin.position).normalized;			// Beregn retningen mod fjenden

			GameObject newProjectile = Instantiate(towerStats.Bullet, shootOrigin.position, Quaternion.LookRotation(direction, Vector3.up));  // Lav en ny projektil
			newProjectile.GetComponent<Bullet>().SetDamage(towerStats.Damage);

			remainingCooldown = towerStats.FireRate;                                                                   // Genstart cooldown
		}
	}
}
