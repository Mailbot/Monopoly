/// By: Adam Pinto
/// The setup for standard properties connecting them to UI
/// 02-10-2017


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The setup for standard properties connecting them to UI
/// </summary>
public class PropertyUISetup : MonoBehaviour {

	public Image m_BackgroundColor;
	public Image m_BorderColor;

	public TextMeshProUGUI m_NameValue;
	public TextMeshProUGUI m_BaseRentValue;
	public TextMeshProUGUI m_1HouseValue;
	public TextMeshProUGUI m_2HouseValue;
	public TextMeshProUGUI m_3HouseValue;
	public TextMeshProUGUI m_4HouseValue;
	public TextMeshProUGUI m_HotelValue;
	public TextMeshProUGUI m_MorgageValue;
	public TextMeshProUGUI m_HouseCostValue;
	public TextMeshProUGUI m_HotelCostValue;

	/// <summary>
	/// Attaching the attrivutes to the right values
	/// </summary>
	/// <param name="a_StandardProperty"></param>
	public void SetupUI(StandardProperty.PropertyCardInformation a_StandardProperty)
	{
		m_BackgroundColor.color = a_StandardProperty.m_CardColor;
		m_BorderColor.color 	= a_StandardProperty.m_CardColor;

		m_NameValue.text 		= a_StandardProperty.m_Name;
		m_BaseRentValue.text 	=  "RENT $" + a_StandardProperty.m_BaseRent.ToString();
		m_1HouseValue.text 		=  "$" + a_StandardProperty.m_1HouseRent.ToString();
		m_2HouseValue.text 		=  "$" + a_StandardProperty.m_2HouseRent.ToString();
		m_3HouseValue.text 		=  "$" + a_StandardProperty.m_3HouseRent.ToString();
		m_4HouseValue.text 		=  "$" + a_StandardProperty.m_4HouseRent.ToString();
		m_HotelValue.text 		=  "$" + a_StandardProperty.m_HotelRent.ToString();
		m_MorgageValue.text 	=  "Morgage Value $" + a_StandardProperty.m_MorgageValue.ToString();
		m_HouseCostValue.text 	=  "$" + a_StandardProperty.m_HousePurchaseCost.ToString();
		m_HotelCostValue.text 	=  "$" + a_StandardProperty.m_HotelPurchaseCost.ToString();

	}
}
