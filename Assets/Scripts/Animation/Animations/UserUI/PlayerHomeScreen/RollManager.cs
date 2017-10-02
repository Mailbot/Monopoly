/// By: Adam Pinto
/// Handles all dice rolling UI windows
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles all dice rolling UI windows
/// </summary>
public class RollManager : CustomAnimation {

	/// <summary>
	/// How long we want to sit and wait for the dice results to be displayed
	/// </summary>
	float c_REVEAL_TIME = 3.0f;

	/// <summary>
	/// Reference of the player we're rolling for
	/// </summary>
	Player m_Player = null;

	/// <summary>
	/// The base panel of the UI we're using
	/// </summary>
	GameObject m_Panel;

	/// <summary>
	/// How long the reveal time has run for
	/// </summary>
	float m_DeltaTime = 0;

	/// <summary>
	/// The current state of this animation
	/// </summary>
	public RollManangerState m_CurrentRollState = RollManangerState.NULL;

	/// <summary>
	/// Creates the window for a specific player
	/// </summary>
	/// <param name="a_Player">The player we want to create this Roll Window UI for</param>
	public RollManager(Player a_Player) 
	{
		//Setting up callbacks
		OnStart 	+= OnStartFunction;
		OnUpdate 	+= OnUpdateFunction;
		OnFinish 	+= OnFinishFunction;

		m_Player 	= a_Player;
	}

	/// <summary>
	/// Creates all the windows and allocates them
	/// Would be better to just turn off and on but being strapped for time, this is safer in creating less bugs
	/// </summary>
	/// <returns></returns>
	public bool OnStartFunction() 
	{
		m_DeltaTime = 0.0f;
		m_CurrentRollState = RollManangerState.NULL;

		//Instiates the window
		m_Panel = Canvas.Instantiate(Resources.Load("RollSreen", typeof(GameObject)) as GameObject);
		m_Panel.transform.parent 		= GameWorldMananger.ReturnInstance().m_Canvas.transform;
		m_Panel.transform.localPosition = Vector3.zero;
		m_Panel.transform.localRotation = Quaternion.identity;

		//Sets up the players name being displayed
		m_Panel.transform.FindChild("Name").GetComponent<TextMeshProUGUI>().text = m_Player.name + "'s Turn!";
		m_Panel.transform.FindChild("RollButton").GetComponent<Button>().onClick.AddListener(ButtonPressed);

		return true;
	}

	/// <summary>
	/// The update function that will move the update through the two stages, before and after they press the roll key
	/// </summary>
	/// <returns>Returns true when the update is finished</returns>
	public bool OnUpdateFunction()
	{
		//Iterating through the states of the RollMananger 
		switch(m_CurrentRollState) 
		{
			//If the button has been pressed, which is caught in a different function, use one frame to setup the display of the dice, had errors when it was on the previous frame
			case RollManangerState.ButtonPress: 
			{
				m_Panel.transform.FindChild("NumberDisplay").FindChild("DiceRoll").GetComponent<TextMeshProUGUI>().text = m_Player.m_LastRoll.ToString();
				m_CurrentRollState = RollManangerState.NumberReveal;
				break;
			}
			//How long we want the player to look at the roll number
			case RollManangerState.NumberReveal: 
			{
				m_DeltaTime += Time.deltaTime;

				if(m_DeltaTime >= c_REVEAL_TIME) 
				{
					GameObject.Destroy(m_Panel);
					GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PlayerMovement(m_Player, m_Player.m_LastRoll));

					return true;
				}

				break;
			}
		}

		return false;
	}

	/// <summary>
	/// Called by the Unity Button class
	/// </summary>
	public void ButtonPressed() 
	{
		//Generates the roll results, 1 - 12 for 2 dices
		m_Player.m_LastRoll = Random.RandomRange(1, 12);

		m_Panel.transform.FindChild("RollButton").gameObject.SetActive(false);
		m_Panel.transform.FindChild("NumberDisplay").gameObject.SetActive(true);

		m_CurrentRollState = RollManangerState.ButtonPress;

		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Rolled: " + m_Player.m_LastRoll);
	}

	//Called on the end
	public bool OnFinishFunction() 
	{
		return false;
	}

	//The different states for the class
	public enum RollManangerState 
	{
		NULL,
		ButtonPress,
		NumberReveal,
	}
}
