/// By: Adam Pinto
/// The node for railroads on the map
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The node for railroads on the map
/// </summary>
public class RailroadProperty : PropertyNode {

	/// <summary>
	/// Information passed to the UISetup script
	/// </summary>
	public RailroadPropertyInformation m_RailroadPropertyInformation;

	/// <summary>
	/// Function for collecting rent, Railroads detrmine rent by how many other railroads are owned, which is found here
	/// </summary>
	/// <param name="a_Player"></param>
	/// <returns></returns>
	public override int OnRent(Player a_Player) 
	{
		//Count of owned Railroads
		int m_TempLinkedPropertyCount = 0;

		foreach(var m_TempGroupedProperty in m_PropertyGroup) 
		{
			if(m_TempGroupedProperty.m_Owner == m_Owner) 
			{
				m_TempLinkedPropertyCount++;
			}
		}

		//The returned rent is based on the ammount of railroads
		switch(m_TempLinkedPropertyCount) 
		{
			case 0: 
			{
				return 25;
			}

			case 1: 
			{
				return 50;
			}

			case 2: 
			{
				return 100;
			}

			case 3: 
			{
				return 200;
			}
		}

		return 0;
	}

	/// <summary>
	/// Creates the GameObject of the card
	/// </summary>
	/// <returns></returns>
	public override GameObject OnDisplay() 
	{
		GameObject m_TempReturnObject = GameObject.Instantiate(Resources.Load("RailroadCard", typeof(GameObject)) as GameObject);
		m_TempReturnObject.GetComponent<PropertyRailroadUISetup>().SetupUI(m_RailroadPropertyInformation);

		return m_TempReturnObject;
	}

	/// <summary>
	/// The information passed to the UI
	/// </summary>
	[System.Serializable]
	public class RailroadPropertyInformation
	{
		public string m_Name;
	}
}
