/// By Adam Pinto
/// Runs the Chance cards
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runs a notication showing the effect of the chance card
/// </summary>
public class Chance : BonusCard {

	/// <summary>
	/// Runs the chance card
	/// </summary>
	/// <param name="a_Player">The player the card will affect</param>
	/// <returns></returns>
	public override bool OnRun(Player a_Player) 
	{ 
		//Randomly choose a card
		int m_TempRandom = Random.Range(0, 10);
		switch(m_TempRandom) 
		{
			case 0: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Advance To Go", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Advance To Go");

				Node m_TempNode = FindDeepChild(GameWorldMananger.ReturnInstance().transform, "Go").GetComponent<Node>();
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(a_Player, HowFarAwayIsNodeFromAnother(this, m_TempNode)));

				break;
			}

			case 1: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Advance to Illinois Ave. - If you pass Go, collect $200", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Advance to Illinois Ave. - If you pass Go, collect $200");

				Node m_TempNode = FindDeepChild(GameWorldMananger.ReturnInstance().transform, "Illinois Avenue").GetComponent<Node>();
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(a_Player, HowFarAwayIsNodeFromAnother(this, m_TempNode)));

				break;
			}

			case 2: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Bank pays you dividend of $50", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Bank pays you dividend of $50");

				a_Player.ModifyMoney(50);

				break;
			}

			case 3: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Go Forward 3 Spaces", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Go Forward 3 Spaces");

				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(a_Player, 3));

				break;
			}

			case 4: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Go directly to Jail – Do not pass Go, do not collect $200", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Go directly to Jail – Do not pass Go, do not collect $200");

				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovementToNode(a_Player, GameWorldMananger.ReturnInstance().transform.FindChild("Jail").GetComponent<Node>(), 1.0f));
				a_Player.m_DaysLeftInJail = GoToJail.c_DAYS_IN_JAIL;

				break;
			}

			case 5: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Make general repairs on all your property – For each property pay $25", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Make general repairs on all your property – For each property pay $25");

				a_Player.ModifyMoney(-a_Player.m_BoughtProperties.Count * 25);

				break;
			}

			case 6: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Pay poor tax of $15", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Pay poor tax of $15");

				a_Player.ModifyMoney(-15);

				break;
			}

			case 7: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Take a trip to Reading Railroad - If you pass Go, collect $200", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Take a trip to Reading Railroad - If you pass Go, collect $200");

				Node m_TempNode = FindDeepChild(GameWorldMananger.ReturnInstance().transform, "Reading Railroad").GetComponent<Node>();
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(a_Player, HowFarAwayIsNodeFromAnother(this, m_TempNode)));

				break;
			}

			case 8: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Take a walk on the Boardwalk – Advance token to Boardwalk - If you pass Go, collect $200", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Take a walk on the Boardwalk – Advance token to Boardwalk - If you pass Go, collect $200");

				Node m_TempNode = FindDeepChild(GameWorldMananger.ReturnInstance().transform, "Boardwalk").GetComponent<Node>();
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(a_Player, HowFarAwayIsNodeFromAnother(this, m_TempNode)));

				break;
			}

			case 9: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: Your building {and} loan matures – Collect $150", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: Your building {and} loan matures – Collect $150");

				a_Player.ModifyMoney(150);

				break;
			}

			case 10: 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Chance Card: You have won a crossword competition - Collect $100", 4.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + a_Player.name + " Chance Card: You have won a crossword competition - Collect $100");

				a_Player.ModifyMoney(100);

				break;
			}
		}
		return true;
	}

	/// <summary>
	/// How many spots would we need to jump in oprder to reach a certain card
	/// </summary>
	/// <param name="a_FirstNode">The starting node</param>
	/// <param name="a_SecondNode">The goal node</param>
	/// <returns></returns>
	public int HowFarAwayIsNodeFromAnother(Node a_FirstNode, Node a_SecondNode) 
	{
		int m_TempCount = 0;

		Node m_CurrentNode = a_FirstNode;


		//Iterate until we've found the goal node
		while(m_CurrentNode != a_SecondNode) {

			m_TempCount++;
			m_CurrentNode = m_CurrentNode.m_NextNode;
		}

		return m_TempCount;
	}

	/// <summary>
	/// Iterate in children to find sub-children, used for finding cards on the board
	/// </summary>
	/// <param name="a_Parent">The top level, the board</param>
	/// <param name="a_Name">The name we're looking for</param>
	/// <returns></returns>
	public static Transform FindDeepChild(Transform a_Parent, string a_Name)
     {
		Transform result = a_Parent.Find(a_Name);

		if (result != null) 
		{
			return result;
		}

		//Recursively find the property we want
		foreach(Transform child in a_Parent)
		{
			result = FindDeepChild(child, a_Name);
			if (result != null)
			{
				return result;
			}
		}

         return null;
     }
}
