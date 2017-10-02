/// By: Adam Pinto
/// The baseclass for purchasable properties that can be possibly mortgaged
/// 02-10-2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UtilityUISetup : MonoBehaviour {

	public TextMeshProUGUI m_NameValue;

	public void SetupUI(UtilityProperty.PropertyUtilityInformation a_PropertyUtilityInformation) {

		m_NameValue.text = a_PropertyUtilityInformation.m_Name;
	}
}
