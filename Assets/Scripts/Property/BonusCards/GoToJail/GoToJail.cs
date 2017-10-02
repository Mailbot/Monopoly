/// By: Adam Pinto
/// Sends a player to jail for a set time
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends a player to jail for a set time
/// </summary>
public class GoToJail : Node 
{
	/// <summary>
	/// How long will the player be sent to jail for
	/// </summary>
	public const int c_DAYS_IN_JAIL = 3;

	/// <summary>
	/// Is called when the player lands on this card, starts an animation that sends to player to the jail node and can't leave until his sentance is served
	/// </summary>
	/// <param name="a_Player">The player we're sending to jail</param>
	/// <returns></returns>
	public override bool OnRun(Player a_Player) 
	{
		GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovementToNode(a_Player, GameWorldMananger.ReturnInstance().transform.FindChild("Jail").GetComponent<Node>(), 1.0f));
		a_Player.m_DaysLeftInJail = c_DAYS_IN_JAIL;
		return true;
	}
}
