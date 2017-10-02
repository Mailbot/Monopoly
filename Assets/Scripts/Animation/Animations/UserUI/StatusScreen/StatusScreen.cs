/// By: Adam Pinto
/// Simple window to show a notification
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Simple window to show a notification
/// </summary>
public class StatusScreen : CustomAnimation {

	/// <summary>
	/// Keep track of how long the notification has been on screen
	/// </summary>
	float m_DeltaTime 	= 0;

	/// <summary>
	/// How long we want the notification on the screen for
	/// </summary>
	float m_MaxDuration = 0;

	/// <summary>
	/// The message we want to display
	/// </summary>
	string m_Message 	= "";

	/// <summary>
	/// The Screen Panel of the Window
	/// </summary>
	GameObject m_Panel 	= null;

	/// <summary>
	/// Creates the notification, requires a message and a duration for it to appear for
	/// </summary>
	/// <param name="a_Message">The message displayed on screen</param>
	/// <param name="a_Duration">The duration of the message on the screen</param>
	public StatusScreen(string a_Message, float a_Duration) 
	{
		//Setting up callbacks
		OnStart 		+= OnStartFunction;
		OnUpdate 		+= OnUpdateFunction;
		OnFinish 		+= OnFinishFunction;

		m_Message 		= a_Message;
		m_MaxDuration 	= a_Duration;
	}

	/// <summary>
	/// Setting up the window scene, creates the window and identifies the pieces it needs
	/// </summary>
	/// <returns></returns>
	public bool OnStartFunction() 
	{
		m_Panel = Canvas.Instantiate(Resources.Load("StatusScreen", typeof(GameObject)) as GameObject);
		m_Panel.transform.parent 		= GameWorldMananger.ReturnInstance().m_Canvas.transform;
		m_Panel.transform.localPosition = Vector3.zero;
		m_Panel.transform.localRotation = Quaternion.identity;

		//The text we'll be setting
		m_Panel.transform.FindChild("Image").FindChild("Text").GetComponent<TextMeshProUGUI>().text = m_Message;
		return false;
	}

	/// <summary>
	/// Display the text on screen for a certian ammount of time
	/// </summary>
	/// <returns></returns>
	public bool OnUpdateFunction() 
	{
		//Counts the duration on the screen
		m_DeltaTime += Time.deltaTime;

		//If exceeds the max time, finish the animation
		if(m_DeltaTime >= m_MaxDuration) 
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// The animation is over, destroy the window
	/// </summary>
	/// <returns></returns>
	public bool OnFinishFunction() 
	{
		GameObject.DestroyImmediate(m_Panel);
		return false;
	}
}
