/// By: Adam Pinto
/// Taxs a player if landed on
/// 02/10/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Taxs a player if landed on
/// </summary>
public class Tax : Node 
{
	//The ammount tax'd defined on the board
	public int m_TaxAmmount;

	/// <summary>
	/// Reduces money on the player
	/// </summary>
	/// <param name="a_Player">Player to which money is reduced</param>
	/// <returns></returns>
	public override bool OnRun(Player a_Player) 
	{
		GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Player " + a_Player.name + " was taxed $" + m_TaxAmmount, 2.0f));
		a_Player.ModifyMoney(-m_TaxAmmount);

		return true;
	}
}
