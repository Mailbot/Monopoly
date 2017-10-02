/// By: Adam Pinto
/// Display the money on the screen
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Displays the money on the screen
/// Not expandable for multiple players but I never made a way to expand the players in game anyway
/// </summary>
public class DisplayMoney : MonoBehaviour 
{
	//Used to change the UI displayed
	public bool m_IsPlayer1 = false;
	//The text we're editing
	public TextMeshProUGUI m_TextToEdit;

	/// <summary>
	/// Sets the money constantly, could be better, could be hooked up to the change event, unfortuentally ran out of time
	/// </summary>
	void Update () 
	{
		if(GameWorldMananger.ReturnInstance().m_Players.Count > 0) 
		{
			if(m_IsPlayer1 == true) {

				m_TextToEdit.text = "Player 1 \n$" + GameWorldMananger.ReturnInstance().m_Players[0].m_Money;
			}
			else
			{
				m_TextToEdit.text = "Player 2 \n$" + GameWorldMananger.ReturnInstance().m_Players[1].m_Money;
			}
		}
	}
}
