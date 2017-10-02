/// By: Adam Pinto
/// The base class for all properties and cards on the board
/// 02/10/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for all properties and cards on the board
/// </summary>
public class Node : MonoBehaviour 
{
	/// <summary>
	/// The next node on the board
	/// </summary>
	public Node m_NextNode;

	/// <summary>
	/// The previous node on the board
	/// </summary>
	public Node m_PreviousNode;

	/// <summary>
	/// The rect describing the area the players icon can land
	/// </summary>
	public Rect m_IconLandingZone;

	/// <summary>
	/// The function that's dervived from sub-class that runs the unique function of all nodes on the board
	/// </summary>
	/// <param name="a_Player">Pass in the player, incase we need it somewhere</param>
	/// <returns>Returns false when not implemented</returns>
	public virtual bool OnRun(Player a_Player) 
	{
		return false;
	}

	/// <summary>
	/// Returns the display card for the node, needs to be unique created for all derived classes
	/// </summary>
	/// <returns></returns>
	public virtual GameObject OnDisplay() 
	{
		return null;
	}

	/// <summary>
	/// Gets a random point in the IconLandingZone
	/// Compensates for the icons width and length, reducing the area it can land in
	/// </summary>
	/// <param name="a_ObjectWidth">The width of the icon</param>
	/// <param name="a_ObjectHeight">The length of the icon</param>
	/// <returns></returns>
	public Vector3 GetPointInRect(float a_ObjectWidth, float a_ObjectHeight) 
	{
		//The centre position of the rectangle
		Vector3 m_TempOffsetPosition = transform.position + new Vector3(m_IconLandingZone.x, 0.0f, m_IconLandingZone.y);

		//The area we've got to work with once we've removed the size of the property
		float m_TempWidth 	= m_IconLandingZone.width - a_ObjectWidth;
		float m_TempHeight 	= m_IconLandingZone.height - a_ObjectHeight;

		//Calculates a random position
		return new Vector3((Random.value * (m_TempWidth * 2.0f)) - m_TempWidth, 0.0f, (Random.value * (m_TempHeight * 2.0f)) - m_TempHeight) + m_TempOffsetPosition;
	}

	/// <summary>
	/// Displays the rectangle and the linked nodes in the scene view
	/// </summary>
	public void OnDrawGizmos() 
	{ 
		Gizmos.color = Color.red;
		Vector3 m_TempOffsetPosition = transform.position + new Vector3(m_IconLandingZone.x, 0.0f, m_IconLandingZone.y);

		//Draing the sides of the rectangl;le
		Gizmos.DrawLine(m_TempOffsetPosition + new Vector3(-m_IconLandingZone.width, 	0.0f, 	 m_IconLandingZone.height), m_TempOffsetPosition + new Vector3( m_IconLandingZone.width, 0.0f,  m_IconLandingZone.height));
		Gizmos.DrawLine(m_TempOffsetPosition + new Vector3(m_IconLandingZone.width, 	0.0f, 	 m_IconLandingZone.height), m_TempOffsetPosition + new Vector3( m_IconLandingZone.width, 0.0f, -m_IconLandingZone.height));
		Gizmos.DrawLine(m_TempOffsetPosition + new Vector3(m_IconLandingZone.width, 	0.0f, 	-m_IconLandingZone.height), m_TempOffsetPosition + new Vector3(-m_IconLandingZone.width, 0.0f, -m_IconLandingZone.height));
		Gizmos.DrawLine(m_TempOffsetPosition + new Vector3(-m_IconLandingZone.width,	0.0f,	-m_IconLandingZone.height), m_TempOffsetPosition + new Vector3(-m_IconLandingZone.width, 0.0f,  m_IconLandingZone.height));

		//If connected to other nodes, draw the line
		if(m_NextNode != null) 
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, m_NextNode.transform.position);
		}

		//If connected to other nodes, draw the line
		if(m_PreviousNode != null) 
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(transform.position + Vector3.up, m_PreviousNode.transform.position + Vector3.up);
		}
	}
}
