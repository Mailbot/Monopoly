/// By: Adam Pinto
/// Moves the player by a set increment
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the player by a set increment
/// </summary>
public class PlayerMovement : CustomAnimation 
{
	/// <summary>
	/// How long the movement will go for, it works on a Lerp so It'll be exact
	/// </summary>
	float c_ANIMATION_SPEED = 1.0f;

	/// <summary>
	/// How many jumps have occured in this animation, used to move exact distances
	/// </summary>
	int m_CurrentJumps 	= 0;

	/// <summary>
	/// How many jumps we want the player to move before stopping
	/// </summary>
	int m_JumpGoal 		= 0;

	/// <summary>
	/// Reference to the player
	/// </summary>
	Player m_Player		= null;

	/// <summary>
	/// A sub animation which is called repeatively to move one node at a time
	/// </summary>
	PlayerMovementToNode m_MovementToNode = null;

	/// <summary>
	/// Creates the animation, the animation requires the Player it's going to move and how many steps to jump
	/// </summary>
	/// <param name="a_Player">The player to be moved</param>
	/// <param name="a_JumpGoal">The ammount of steps to jump</param>
	public PlayerMovement(Player a_Player, int a_JumpGoal) {

		//Subscribe updates to the events on the animation
		OnStart 	+= OnStartFunction;
		OnUpdate 	+= OnUpdateFunction;
		OnFinish 	+= OnFinishFunction;

		//Keep reference of the inputed data
		m_JumpGoal 	 = a_JumpGoal;
		m_Player 	 = a_Player;

		//Create a sub animation that attempts to move to the next node
		m_MovementToNode = new PlayerMovementToNode(m_Player, m_Player.m_CurrentNode.m_NextNode, c_ANIMATION_SPEED);
	}

	/// <summary>
	/// Called at the start, used to setup / default values
	/// </summary>
	/// <returns></returns>
	public bool OnStartFunction() 
	{
		m_CurrentJumps = 0;
		m_MovementToNode.RunStart();

		return false;
	}

	/// <summary>
	/// Update function used to check if the animation that's currently being run has finished, if it has, check if enough spots have moved, if so quit the animation.
	/// Otherwise, repeat
	/// </summary>
	/// <returns>Returns true if finished</returns>
	public bool OnUpdateFunction()
	{
		//Checks the sub-animation thats moving to the next node has finished
		if(m_MovementToNode.RunUpdate() == true) 
		{
			//If it has finished, increase the jumps
			m_CurrentJumps++;

			//Check if the jumps is greater then the goal, if it is, break out of the animation
			if(m_CurrentJumps >= m_JumpGoal) 
			{
				return true;
			}

			//If we didn't break previous that means we need to do another lap, recreate the subanimation
			m_MovementToNode = new PlayerMovementToNode(m_Player, m_Player.m_CurrentNode.m_NextNode, c_ANIMATION_SPEED);
			m_MovementToNode.RunStart();
		}
		
		//Didn't finish, return false
		return false;
	}

	/// <summary>
	/// The animation has finished, log the resulting location and run the OnRun of the node, calling its functionality
	/// </summary>
	/// <returns></returns>
	public bool OnFinishFunction() 
	{
		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Landed On: " + m_Player.m_CurrentNode.name);
		m_Player.m_CurrentNode.OnRun(m_Player);
		return false;
	}
}
