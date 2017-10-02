/// By: Adam Pinto
/// The baseclass for purchasable properties that can be possibly mortgaged
/// 02-10-2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityProperty : PropertyNode 
{
	public PropertyUtilityInformation m_PropertyUtilityInformation;
	public override int OnRent(Player a_Player) 
	{
		int m_TempLinkedPropertyCount = 0;

		foreach(var m_TempGroupedProperty in m_PropertyGroup) 
		{
			if(m_TempGroupedProperty.m_Owner == m_Owner) 
			{
				m_TempLinkedPropertyCount++;
			}
		}

		if(m_TempLinkedPropertyCount == 0) 
		{
			return a_Player.GetLastRoll() * 4;
		}
		else if(m_TempLinkedPropertyCount == 1)
		{
			return a_Player.GetLastRoll() * 10;
		}

		return 0;
	}

	public override GameObject OnDisplay() 
	{
		GameObject m_TempReturnObject = GameObject.Instantiate(Resources.Load("UtilityCard", typeof(GameObject)) as GameObject);
		m_TempReturnObject.GetComponent<UtilityUISetup>().SetupUI(m_PropertyUtilityInformation);

		return m_TempReturnObject;
	}

	[System.Serializable]
	public class PropertyUtilityInformation
	{
		public string m_Name;
	}
}
