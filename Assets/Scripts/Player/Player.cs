/// By: Adam Pinto
/// Keeps track of information about the player
/// 02/10/2017
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of information about the player
/// </summary>
public class Player : MonoBehaviour 
{
	/// <summary>
	/// The money the player has, starts at 1500
	/// </summary>
	public int m_Money = 1500;

	/// <summary>
	/// The current node the player is on
	/// </summary>
	public Node m_CurrentNode = null;

	/// <summary>
	/// The value the player rolled last
	/// </summary>
	[HideInInspector]
	public int m_LastRoll = 0;

	/// <summary>
	/// How many Get Out of Jail cards the player has
	/// </summary>
	[HideInInspector]
	public int m_GetOutOfJailCards = 0;

	/// <summary>
	/// How many days left in jail the player has, if in jail
	/// </summary>
	[HideInInspector]
	public int m_DaysLeftInJail = 0;

	/// <summary>
	/// The list of properties the player has bought
	/// </summary>
	/// <returns></returns>
	[HideInInspector]
	public List<PropertyNode> m_BoughtProperties = new List<PropertyNode>();

	/// <summary>
	/// Transfers money from one player to the next
	/// 
	/// Fixes: If time, check if player can't afford and start morgaging properties
	/// </summary>
	/// <param name="a_PlayerToTransfer">Where the money is going/param>
	/// <param name="a_MoneyToTransfer">How much Money</param>
	public void TransferMoney(Player a_PlayerToTransfer, int a_MoneyToTransfer) 
	{
		m_Money -= a_MoneyToTransfer;
		a_PlayerToTransfer.m_Money += a_MoneyToTransfer;
	}

	/// <summary>
	/// Change the money on this player
	/// </summary>
	/// <param name="a_Ammount">The ammount to change</param>
	public void ModifyMoney(int a_Ammount) 
	{
		m_Money += a_Ammount;
	}

	/// <summary>
	/// Returns the last roll
	/// </summary>
	/// <returns>The last roll affecting this player</returns>
	public int GetLastRoll() 
	{
		return m_LastRoll;
	}
}
