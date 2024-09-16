using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
	public static GameController Instance;			// Laver �t objekt ud af GameController-klassen kaldet Instance
	public int lifes;                               // Antal liv
	public int gold;
	public TextMeshProUGUI lifesScreenText;
	public TextMeshProUGUI goldScreenText; // Teksten p� sk�rmen

	private void Awake()							// Awake er ligesom Start-metoden, den k�rer bare tidligere
	{
		Instance = this;                            // Instance = denne klasse (GameController)
		gold = 1000;
	}

    private void Update()
    {
		goldScreenText.text = $"Gold: {gold}";
	}
    public void LoseLife()
	{
		lifes = lifes - 1;                          // Man kan ogs� skrive "lifes--", det giver det samme
		lifesScreenText.text = $"Lifes: {lifes}";   // S�tter v�rdien af teksten p� sk�rmen til at vise liv
		if ( lifes < 1)
		{
			print("Game Lost!");
			Time.timeScale = 0;						// S�tter tiden i st�. 1 = normaltid.
		}
	}


}
