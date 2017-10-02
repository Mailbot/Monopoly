/// By: Adam Pinto
/// The setup for displaying railroad cards, connecting information to UI
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The setup for displaying railroad cards, connecting information to UI
/// </summary>
public class PropertyRailroadUISetup : MonoBehaviour 
{
	public TextMeshProUGUI m_NameValue;

	public void SetupUI(RailroadProperty.RailroadPropertyInformation a_PropertyUtilityInformation) 
	{
		m_NameValue.text = a_PropertyUtilityInformation.m_Name;
	}
}
