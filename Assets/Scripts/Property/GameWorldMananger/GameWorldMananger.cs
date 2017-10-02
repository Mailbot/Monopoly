/// By: Adam Pinto
/// The Mananger that runs the game and keeps everything updating
/// 02-10-2017

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Mananger that runs the game and keeps everything updating
/// This is also the gameboard, containing all the objects and nodes
/// 
/// Handles all animations, has a List of all queue animations, if list is empty, move to next player
/// </summary>
public class GameWorldMananger : MonoBehaviour 
{
	/// <summary>
	/// Static instance used to access this mananger from a singleton
	/// </summary>
	private static GameWorldMananger m_Instance = null;

	/// <summary>
	/// Returns the single instance of this object, needs to be placed in the scene
	/// Will search in the scene once for it
	/// </summary>
	/// <returns>Returns the single instance</returns>
	public static GameWorldMananger ReturnInstance() 
	{
		if(m_Instance == null) 
		{
			m_Instance = GameObject.FindObjectOfType<GameWorldMananger>();
		}

		return m_Instance;
	}

	/// <summary>
	/// The players in the game, can be increased as much as wanted
	/// </summary>
	public List<Player> m_Players = new List<Player>();

	/// <summary>
	/// Which player of the m_Players list is currently running
	/// </summary>
	public int m_CurrentPlayer = 0;

	/// <summary>
	/// List of all animations currently run, if emopty, it means its the next players turn
	/// </summary>
	public Queue<CustomAnimation> m_Animations = new Queue<CustomAnimation>();

	/// <summary>
	/// Keeping a reference to the Go node
	/// </summary>
	Go m_GoNode = null;

	/// <summary>
	/// Keeping a reference to the UI Canvas
	/// </summary>
	public GameObject m_Canvas;

	/// <summary>
	/// Creates and intilizses the players and the gameboard
	/// </summary>
	public void Awake() 
	{ 
		WriteToLog("Start Of Session");

		m_GoNode = transform.FindChild("Go").GetComponent<Go>();
		m_Canvas = GameObject.Find("Canvas").gameObject;

		//Creates the players and sets their position on Go
		foreach(var m_TempPlayer in m_Players) {

			m_TempPlayer.transform.position = m_GoNode.GetPointInRect(m_TempPlayer.transform.localScale.x * 0.5f, m_TempPlayer.transform.localScale.z * 0.5f);
			m_TempPlayer.m_CurrentNode 		= m_GoNode;
		}
	}

	/// <summary>
	/// Main update running the game
	/// 
	/// Manages all animations
	/// </summary>
	public void Update() 
	{
		//Quits Application
		if(Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit();
		}

		//If we have an animation in the list, call its update, if it returns true, it's finished and wants to be dequeued and remove from the currently updating list
		if(m_Animations.Count > 0) 
		{
			if(m_Animations.Peek().RunUpdate() == true) 
			{
				m_Animations.Dequeue();

				if(m_Animations.Count > 0) {

					//If there's another animation, prep it to run
					m_Animations.Peek().RunStart();
				}
			}
		}
		else //If we dont have anymore animations, attempt to move to the next player
		{
			//Keep a reference of the next player
			Player m_CurrentPlayer = m_Players[NextPlayer()];

			//If he doesn't have any jail-time, load up the dice roll animation for him
			if(m_CurrentPlayer.m_DaysLeftInJail <= 0) 
			{
				AddToAnimationQueue(new RollManager(m_CurrentPlayer));
				m_Animations.Peek().RunStart();
			}
			else //If he does have jail time, post a message displaying the jail time and deincrement his jailtime by a day
			{
				CustomAnimation m_TempCustomAnimation = new StatusScreen("Spent the Day in Jail - Wait " + m_CurrentPlayer.m_DaysLeftInJail + " Days For Release", 2.0f);
				m_TempCustomAnimation.RunStart();

				AddToAnimationQueue(m_TempCustomAnimation);
				WriteToLog("Player: " + m_CurrentPlayer.name + " Spent the day in Jail");
				m_CurrentPlayer.m_DaysLeftInJail--;

				//If the player has moved out of jail, move him to visiting
				if(m_CurrentPlayer.m_DaysLeftInJail <= 0) 
				{
					m_TempCustomAnimation = new PlayerMovementToNode(m_CurrentPlayer, transform.FindChild("Just Visiting").GetComponent<Node>(), 1.0f);
					m_TempCustomAnimation.RunStart();

					AddToAnimationQueue(m_TempCustomAnimation);
				}
			}
		}
	}

	//Adds an animation to the queue to be played
	public void AddToAnimationQueue(CustomAnimation a_Animation) 
	{
		m_Animations.Enqueue(a_Animation);
	}

	//Writes to the log
	public void WriteToLog(string a_String) {

		File.AppendAllText("Log.txt", DateTime.Now.ToString() + ": " + a_String + Environment.NewLine);
	}

	//Increments the player
	public int NextPlayer() 
	{
		m_CurrentPlayer++;

		if(m_CurrentPlayer >= m_Players.Count) 
		{
			m_CurrentPlayer = 0;
		}

		return m_CurrentPlayer;
	}
}
