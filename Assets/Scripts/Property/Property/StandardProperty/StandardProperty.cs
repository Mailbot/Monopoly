/// By: Adam Pinto
/// The class for standard properties on the map
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class for standard properties on the map
/// </summary>
public class StandardProperty : PropertyNode 
{
	/// <summary>
	/// The information about the card that will be passed to the UI
	/// </summary>
	public PropertyCardInformation m_PropertyCardInformation;

	/// <summary>
	/// The ammount of houses purchased, includes hotels
	/// </summary>
	[HideInInspector]
	public int m_PurchasedHouses = 0;

	/// <summary>
	/// Determines the rent based on the rules for standard properties
	/// </summary>
	/// <param name="a_Player">The player sending rent</param>
	/// <returns>Returns the rent</returns>
	public override int OnRent(Player a_Player) 
	{
		//Make sure this property has an owner
		if(m_Owner != null) {

			//Check how many houses / apartments this property hass
			//Return the approiate value
			switch(m_PurchasedHouses) {

				case 0: 
				{
					//If no houses are bought for this house, check if all other properties of this group have been bought by this owner, if so, double the rent for no-houses
					if(m_PurchasedHouses == 0) {
						
						foreach(var m_TempPropertyStandard in m_PropertyGroup) {

							if(m_TempPropertyStandard.m_Owner != m_Owner) {

								return m_PropertyCardInformation.m_BaseRent;
							}
						}

						return m_PropertyCardInformation.m_BaseRent * 2;
					}

					break;
				}

				case 1: 
				{
					return m_PropertyCardInformation.m_1HouseRent;
					break;
				}

				case 2: 
				{
					return m_PropertyCardInformation.m_2HouseRent;
					break;
				}

				case 3: 
				{
					return m_PropertyCardInformation.m_3HouseRent;
					break;
				}

				case 4: 
				{
					return m_PropertyCardInformation.m_4HouseRent;
					break;
				}

				case 5: 
				{
					return m_PropertyCardInformation.m_HotelRent;
					break;
				}
			}
		}
		
		return 0;
	}

	/// <summary>
	/// Returns the card setup and ready to use
	/// </summary>
	/// <returns>The card for this property</returns>
	public override GameObject OnDisplay() 
	{
		GameObject m_TempReturnObject = GameObject.Instantiate(Resources.Load("StandardPropertyCard", typeof(GameObject)) as GameObject);
		m_TempReturnObject.GetComponent<PropertyUISetup>().SetupUI(m_PropertyCardInformation);

		return m_TempReturnObject;
	}

	/// <summary>
	/// The information passed to the UI
	/// </summary>
	[System.Serializable]
	public class PropertyCardInformation 
	{
		public string m_Name;
		public Color m_CardColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
		public int m_BaseRent;
		public int m_1HouseRent;
		public int m_2HouseRent;
		public int m_3HouseRent;
		public int m_4HouseRent;
		public int m_HotelRent;
		public int m_MorgageValue;
		public int m_HousePurchaseCost;
		public int m_HotelPurchaseCost;
	}
}
