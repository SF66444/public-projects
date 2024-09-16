using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform goal;
    public float maxHP;
    public float currentHP;
    public Image hpBar;
    public Image hpBarBackground;
	private Camera mainCamera;

	void Start()
    {
        mainCamera = Camera.main;
        goal = GameObject.Find("Goal").transform;           // goal-variablen gives en værdi. Scriptet finder selv "Goal" i Unity-scenen når denne linje køres.
        agent.SetDestination(goal.position);
        currentHP = maxHP;
    }

	private void Update()
	{
        if (currentHP <= 0)                                 // Har fjenden 0 eller under 0 hp?
        {
            GameController.Instance.gold += 100;
            Destroy(gameObject);                            // Fjern fjenden fra spillet
        }

        hpBar.fillAmount = (float)currentHP / maxHP;        // Sætter hp-barens værdi.

		hpBar.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);           // HP baren peger i retning af kameraet
		hpBarBackground.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up); // HP barens baggrundsbillede peger i retning af kameraet
	}
}
