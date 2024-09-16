using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public TowerStats towerStats;
	public List<GameObject> enemiesInRange = new List<GameObject>();            // En liste der skal indeholde fjender som t�rnet kan skyde p�
	float remainingCooldown;                                                    // Tiden f�r n�ste skud
	public Transform shootOrigin;                                               // Stedet projektilerne skal spawne

	void Start()
	{

	}

	void Update()
	{
		enemiesInRange.RemoveAll(enemy => enemy == null);                       // Ryd op i listen og fjern �delagte fjender (null-referencer)

		remainingCooldown = remainingCooldown - Time.deltaTime;                 // remaining cooldown f�r trukket lige s� meget fra sig som den tid sidste frame varede

		if (remainingCooldown <= 0)                                             // er tiden g�et?
		{
			if (enemiesInRange.Count > 0)                                       // er der fjender som t�rnet m� skyde p�?
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

	private void OnTriggerEnter(Collider other)						// N�r noget r�r tower's trigger
	{
		if (other.gameObject.name == "Enemy(Clone)")                // Hedder objektet der r�r "Enemy(Clone)"?
		{
			enemiesInRange.Add(other.gameObject);                   // Tilf�j til listen enemiesInRange
		}
	}

	private void OnTriggerExit(Collider other)							// N�r noget forsvinder fra tower's trigger
	{
		if (other.gameObject.name == "Enemy(Clone)")					// Hedder objektet der r�r "Enemy(Clone)"?
		{
			enemiesInRange.Remove(other.gameObject);					// Fjern fra listen enemiesInRange
		}
	}

	void Shoot()
	{
		
		if (enemiesInRange.Count > 0)																		// S�rg for at der er en fjende at skyde p�
		{
			GameObject targetEnemy = enemiesInRange[0];														// Find den f�rste fjende i listen
			Vector3 direction = (targetEnemy.transform.position - shootOrigin.position).normalized;			// Beregn retningen mod fjenden

			GameObject newProjectile = Instantiate(towerStats.Bullet, shootOrigin.position, Quaternion.LookRotation(direction, Vector3.up));  // Lav en ny projektil
			newProjectile.GetComponent<Bullet>().SetDamage(towerStats.Damage);

			remainingCooldown = towerStats.FireRate;                                                                   // Genstart cooldown
		}
	}
}
