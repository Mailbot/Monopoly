/// By: Adam Pinto
/// Purpose: To move directly to another node smoothly without hoping spaces, with an ark
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To move directly to another node smoothly without hoping spaces, with an ark
/// </summary>
public class PlayerMovementToNode : CustomAnimation {

	/// <summary>
	/// The Ark hieght when hoping
	/// </summary>
	const float c_MAX_HEIGHT = 2.0f;

	/// <summary>
	/// The player we'll be hoping
	/// </summary>
	Player 	m_Player;

	/// <summary>
	/// The node we're moving too
	/// </summary>
	Node	m_Node;

	/// <summary>
	/// The original location of the Player
	/// </summary>
	Vector3 m_StartPos;

	/// <summary>
	/// The location we want to move too
	/// </summary>
	Vector3 m_EndPos;

	/// <summary>
	/// How long the animation has run for
	/// </summary>
	float m_DeltaTime = 0;

	/// <summary>
	/// How much time we've got for it to run for
	/// </summary>
	float m_MaxTime;

	/// <summary>
	/// Setup for the animation, takes the player we want top move, the node we want to move too and how long we want it to take
	/// </summary>
	/// <param name="a_Player">The player we want to move</param>
	/// <param name="a_Node">The Node we want to jump too</param>
	/// <param name="a_AnimationDuration">How long it will take</param>
	public PlayerMovementToNode(Player a_Player, Node a_Node, float a_AnimationDuration) 
	{
		//Setting up callbacks
		OnStart 	+= OnStartFunction;
		OnUpdate 	+= OnUpdateFunction;
		OnFinish 	+= OnFinishFunction;

		m_Player 	= a_Player;
		m_Node 		= a_Node;
		m_MaxTime	= a_AnimationDuration;
	}

	/// <summary>
	/// Defaulting values
	/// </summary>
	/// <returns></returns>
	public bool OnStartFunction() 
	{
		m_DeltaTime = 0.0f;
		m_StartPos 	= m_Player.transform.position;
		m_EndPos 	= m_Node.GetPointInRect(m_Player.transform.localScale.x * 0.5f, m_Player.transform.localScale.z * 0.5f);
		return true;
	}

	/// <summary>
	/// The update function increments the current duration of the animation and moves the players position based on a sin curve to the next node
	/// Once reached, the animation returns true
	/// </summary>
	/// <returns></returns>
	public bool OnUpdateFunction()
	{
		//The current length of the movement
		m_DeltaTime += Time.deltaTime;

		//If it's exceeded the max time, end the update
		if(m_DeltaTime >= m_MaxTime) {

			return true;
		}

		//Convert the time with the max time to a 0 - 1 value so we can then multiply it by pi (The peak on the sinwave) and modify the altitude by multiplying by a max height
		float m_TempNormalizedDelta = (m_DeltaTime / m_MaxTime);
		m_Player.transform.position = Vector3.Lerp(m_StartPos, m_EndPos, m_TempNormalizedDelta) + new Vector3(0.0f, Mathf.Sin(m_TempNormalizedDelta * Mathf.PI) * c_MAX_HEIGHT, 0.0f);

		//If reached here the animation isn't over
		return false;
	}

	/// <summary>
	/// Final step, also handles collecting money from Go
	/// </summary>
	/// <returns></returns>
	public bool OnFinishFunction() 
	{
		// corrects for overshooting / error
		m_Player.transform.position = m_EndPos;
		m_Player.m_CurrentNode 		= m_Node;

		//Create a switch for things that need to activate when passing
		switch(m_Node.name) 
		{
			// If we pass go, we want to be reqwarded money and a notifcation
			case "Go": 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new StatusScreen("Passed Go Collect $200", 2.0f));
				GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Obtained $200 from Go");
				m_Player.ModifyMoney(200);
				break;
			}
		}

		return false;
	}
}
