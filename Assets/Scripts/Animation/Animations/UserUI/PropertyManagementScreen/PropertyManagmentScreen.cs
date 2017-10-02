/// By: Adam Pinto
/// Used to display properties and give options on what the user can do with them
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Used to display properties and give options on what the user can do with them
/// </summary>
public class PropertyManagmentScreen : CustomAnimation {

	/// <summary>
	/// The Player we plan to give the options too
	/// </summary>
	Player m_Player = null;

	/// <summary>
	/// The property we're displaying managment options for
	/// </summary>
	PropertyNode m_PropertyNode = null;

	/// <summary>
	/// The Base panel for the UI Screen
	/// </summary>
	GameObject m_Panel;

	/// <summary>
	/// The property card we're displaying
	/// </summary>
	GameObject m_Card;

	/// <summary>
	/// The state of the entire window, mainly used to keep track of when it should close
	/// </summary>
	public PropertyManagmentStates m_CurrentPropertyState = PropertyManagmentStates.NULL;

	/// <summary>
	/// The state of the functionality the window is providing
	/// </summary>
	public PropertyManagmentFunctionality m_CurrentFunctionality = PropertyManagmentFunctionality.NULL;

	/// <summary>
	/// The setup code for the PropertyManagmentScreen, takes the player we want to give options too, the property we're affecting and the functionality we want to perform
	/// The function is what affect we want to do to the property, are we collecting rent or purchasing it for example
	/// </summary>
	/// <param name="a_Player">The player managing the property</param>
	/// <param name="a_PropertyNode">The property being affected</param>
	/// <param name="a_Functionality">The affect on the property</param>
	public PropertyManagmentScreen(Player a_Player, PropertyNode a_PropertyNode, PropertyManagmentFunctionality a_Functionality) 
	{
		//Setting up callbacks
		OnStart 		+= OnStartFunction;
		OnUpdate 		+= OnUpdateFunction;
		OnFinish 		+= OnFinishFunction;

		m_Player 		= a_Player;
		m_PropertyNode 	= a_PropertyNode;

		m_CurrentFunctionality = a_Functionality;
	}

	/// <summary>
	/// Setting up windows and button callbacks based on the functionality
	/// </summary>
	/// <returns></returns>
	public bool OnStartFunction() 
	{
		//Create the UI window
		m_Panel = Canvas.Instantiate(Resources.Load("PropertyManagementScreen", typeof(GameObject)) as GameObject);
		m_Panel.transform.parent 		= GameWorldMananger.ReturnInstance().m_Canvas.transform;
		m_Panel.transform.localPosition = Vector3.zero;
		m_Panel.transform.localRotation = Quaternion.identity;

		//Create the card from the property, the property will return it filled out and ready for use
		m_Card = m_PropertyNode.OnDisplay();
		m_Card.transform.parent 		= m_Panel.transform;
		m_Card.transform.localPosition 	= new Vector3(0.0f, 115.0f, 0.0f);
		m_Card.transform.localRotation 	= Quaternion.identity;

		//Find the "Close" button
		m_Panel.transform.FindChild("Close").GetComponent<Button>().onClick.AddListener(CloseWindow);

		//Keep track of the button and the text inside as we'll be modyfying it heavily
		Button 	m_TempButton 	= m_Panel.transform.FindChild("Button").GetComponent<Button>();
		Text	m_tempText		= m_TempButton.transform.FindChild("Text").GetComponent<Text>();

		//Based on the functionality we require, modyfy the button
		switch(m_CurrentFunctionality)
		{
			//If we want to purchase the property, set the button up accordingly
			case PropertyManagmentFunctionality.PurchaseProperty: 
			{
				if(m_Player.m_Money >= m_PropertyNode.m_PurchaseCost) 
				{
					m_TempButton.onClick.AddListener(ButtonPressPurchaseProperty);
					m_tempText.text = "Purchase Property: $" + m_PropertyNode.m_PurchaseCost;
				}
				else //Checks if we can afford to purchase it
				{
					m_TempButton.onClick.AddListener(CloseWindow);
					m_tempText.text = "Cannot Afford: $" + m_PropertyNode.m_PurchaseCost + "/n Click To Close";
				}

				break;
			}

			//If we want to pay rent on the property, set the button up accordingly
			case PropertyManagmentFunctionality.PayRent: 
			{
				if(m_Player.m_Money >= m_PropertyNode.OnRent(m_Player)) 
				{
					m_Panel.transform.FindChild("Close").gameObject.SetActive(false);;

					m_TempButton.onClick.AddListener(ButtonPressPayRent);
					m_tempText.text = "Pay Rent: $" + m_PropertyNode.OnRent(m_Player);
				}
				else //Checks if we can afford the rent, if we can't, mortgage properties or create an end state
				{
					//Player Loses
				}

				break;
			}

			//If we want to upgrade the property, set the button up accordingly
			case PropertyManagmentFunctionality.PurchaseUpgrade: 
			{
				m_TempButton.onClick.AddListener(ButtonPressPurchaseUpgrade);
				m_tempText.text = "Upgrade Property: $50";

				break;
			}
		}

		return true;
	}

	/// <summary>
	/// Basic update, used to see if we need to close the window, we may do it at different states
	/// </summary>
	/// <returns></returns>
	public bool OnUpdateFunction()
	{
		if(m_CurrentPropertyState == PropertyManagmentStates.CloseWindow) 
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Purchases the property for the player
	/// </summary>
	public void ButtonPressPurchaseProperty() 
	{
		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Bought: " + m_Player.m_CurrentNode.name);
		m_PropertyNode.OnPurchase(m_Player);
		m_CurrentPropertyState = PropertyManagmentStates.CloseWindow;
	}

	/// <summary>
	/// Causes the property to upgrade
	/// </summary>
	public void ButtonPressPurchaseUpgrade() 
	{
		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Upgraded: " + m_Player.m_CurrentNode.name);
		m_CurrentPropertyState = PropertyManagmentStates.CloseWindow;
	}

	/// <summary>
	/// Pay the rent
	/// </summary>
	public void ButtonPressPayRent() 
	{
		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Payed $" + m_PropertyNode.OnRent(m_Player) +  " to " + m_PropertyNode.m_Owner.name + " rent for staying at: " + m_Player.m_CurrentNode.name);
		m_PropertyNode.m_Owner.TransferMoney(m_Player, m_PropertyNode.OnRent(m_Player));
		m_CurrentPropertyState = PropertyManagmentStates.CloseWindow;
	}

	/// <summary>
	/// Close the window
	/// </summary>
	public void CloseWindow() 
	{
		GameWorldMananger.ReturnInstance().WriteToLog("Player: " + m_Player.name + " Did not interact with: " + m_Player.m_CurrentNode.name);
		m_CurrentPropertyState = PropertyManagmentStates.CloseWindow;
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

	/// <summary>
	/// The states for the window
	/// </summary>
	public enum PropertyManagmentStates 
	{
		NULL,
		ButtonPress,
		CloseWindow,
	}

	/// <summary>
	/// The states for the functionality of the window
	/// </summary>
	public enum PropertyManagmentFunctionality {

		NULL,
		PurchaseProperty,
		PurchaseUpgrade,
		PayRent,

	}
}
