/// By: Adam Pinto
/// Very simple camera script to rotate the camera arround the board
/// 02-10-2017 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Very simple camera script to rotate the camera arround the board
/// </summary>
public class CameraManager : MonoBehaviour {
	
	/// <summary>
	/// Rotates the camera arround the board
	/// </summary>
	void Update () {
		
		///Gets the direction of the centre point to the goal rotation
		Vector3 m_Direction = (GameWorldMananger.ReturnInstance().m_Players[GameWorldMananger.ReturnInstance().m_CurrentPlayer].transform.position - transform.position).normalized;
		//rotate the direction towards it using a contiously increasing lerp value to smooth the movement
		transform.forward = Vector3.Lerp(transform.forward, m_Direction, Time.deltaTime * 3.0f);
		//Undo rotations to axis we dont want rotated
		transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);

	}
}
