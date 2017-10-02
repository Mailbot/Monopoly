//By: Adam Pinto
//Used to control items that are required to be run in sequence and with multiple steps
//02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to control items that are required to be run in sequence and with multiple steps
///	I chose to use delegates here with the idea that I might be subscribing multiple functions to events, instead of just inheriting everything,
/// In retrospect, I didn't end up doing that as much as I thought and a derived sollution may have been less complicaged
/// </summary>
public class CustomAnimation
{
	/// <summary>
	/// The current state of the animation, used to determine what functions to call and in what order
	/// </summary>
	public CustomAnimationCurrentState m_CurrentState;

	/// <summary>
	/// A delegate we'll be using to subscribe other funtions to events
	/// </summary>
	/// <returns></returns>
	public delegate bool AnimationSubscriptionDelegate();

	/// <summary>
	/// Called Once at the start of an animation
	/// </summary>
	public AnimationSubscriptionDelegate OnStart;
	/// <summary>
	/// Called repeatedly during an animation until the function returns a True (To break out)
	/// </summary>
	public AnimationSubscriptionDelegate OnUpdate;
	/// <summary>
	/// Called Once at the end of an animation
	/// </summary>
	public AnimationSubscriptionDelegate OnFinish;

	/// <summary>
	/// Starts the animation, needs to be called first
	/// Same effect can happen by just setting the current state
	/// </summary>
	public void RunStart() 
	{
		if(m_CurrentState == CustomAnimationCurrentState.NULL) 
		{
			m_CurrentState = CustomAnimationCurrentState.OnStart;
		}
	}

	/// <summary>
	/// Runs the animation, as this isn't a monoobject we'll need to provide the update step
	/// </summary>
	/// <returns>Returns true if the animation has been completely finished, if the OnFinish function has been called</returns>
	public bool RunUpdate() 
	{
		//The main loop on animations
		switch(m_CurrentState) 
		{
			//Called once and instantly parmed off to the OnUpdate stage
			case CustomAnimationCurrentState.OnStart: 
			{
				OnStart();
				m_CurrentState = CustomAnimationCurrentState.OnUpdate;
				break;
			}
			//Calls OnUpdate and listens for a returned true, if it gets it, move to the finish stage
			case CustomAnimationCurrentState.OnUpdate: 
			{
				if(OnUpdate() == true)
				{
					m_CurrentState = CustomAnimationCurrentState.OnFinish;
				}
				break;
			}
			//Called once at the end
			case CustomAnimationCurrentState.OnFinish: 
			{
				OnFinish();
				m_CurrentState = CustomAnimationCurrentState.NULL;
				return true;
			}
		}

		//In all other stages except the very last update, return false
		return false;
	}

	//Enum of all teh states
	//NULL is the default and means the animation is currently not playing
	public enum CustomAnimationCurrentState
	{
		NULL,
		OnStart,
		OnUpdate,
		OnFinish,
	}
}
