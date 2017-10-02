/// By: Adam Pinto
/// The baseclass for purchasable properties that can be possibly mortgaged
/// 02-10-2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The baseclass for purchasable properties that can be possibly mortgaged
/// </summary>
public class PropertyNode : Node 
{
	/// <summary>
	/// The cost to purchase this property
	/// </summary>
	public float m_PurchaseCost = 0.0f;

	/// <summary>
	/// The money recieved if morgaged
	/// </summary>
	public float m_Mortgage 	= 0.0f;

	/// <summary>
	/// Is this property Mortgaged?
	/// </summary>
	public bool m_IsMorgaged	= false;

	/// <summary>
	/// The group of connected properties
	/// Properties connected by color or utility
	/// </summary>
	/// <returns></returns>
	public List<PropertyNode> m_PropertyGroup = new List<PropertyNode>();

	/// <summary>
	/// The current owner of this property, if none, this is null
	/// </summary>
	[HideInInspector]
	public Player m_Owner = null;

	//Default OnRun for all purchasable properties as they function similarishly 
	public override bool OnRun(Player a_Player) 
	{
		//If no owner, offer to purchase the property
		if(m_Owner == null) 
		{
			GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PropertyManagmentScreen(a_Player, this, PropertyManagmentScreen.PropertyManagmentFunctionality.PurchaseProperty));
		}
		else
		{
			//If an owner, if not the player that just landed on the node, then ask for rent
			if(m_Owner != a_Player) 
			{
				GameWorldMananger.ReturnInstance().AddToAnimationQueue(new PropertyManagmentScreen(a_Player, this, PropertyManagmentScreen.PropertyManagmentFunctionality.PayRent));
			}
			else //Otherwise load up the management tab, for upgrading
			{
				OnManange();
			}
		}

		return true;
	}

	/// <summary>
	/// Virtual function for retrieving rent as some properties identify rent differently
	/// </summary>
	/// <param name="a_Player">The player that we'll be retrieving rent from</param>
	/// <returns>The ammount of rent required</returns>
	public virtual int OnRent(Player a_Player) 
	{
		return 0;
	}

	/// <summary>
	/// Default function for purchaing a property, can be overriden if needed
	/// </summary>
	/// <param name="a_NewOwner">The new owner of the property</param>
	/// <returns></returns>
	public virtual bool OnPurchase(Player a_NewOwner) 
	{
		a_NewOwner.m_BoughtProperties.Add(this);
		a_NewOwner.ModifyMoney(-(int)m_PurchaseCost);
		m_Owner = a_NewOwner;
		return true;
	}

	/// <summary>
	/// Used to display different manage rules as not all properties can have houses / hotels
	/// </summary>
	/// <returns></returns>
	public virtual bool OnManange() {

		return false;
	}

	/// <summary>
	/// default function for mortgage properties, can be overriten if needed
	/// </summary>
	/// <returns></returns>
	public virtual bool OnMorgage() 
	{
		if(m_Owner != null && m_IsMorgaged == false) 
		{
			m_IsMorgaged = true;
			m_Owner.ModifyMoney((int)(m_Mortgage));
		}

		return true;
	}
}