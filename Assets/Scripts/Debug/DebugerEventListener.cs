using UnityEngine;


public class DebugerEventListener : MonoBehaviour
{
	//**************************************************************************
	//***** Functions **********************************************************

	public void OnPlayerWin()
	{
		Debug.Log("Player Win");
	}

	public void OnCollect()
	{
		Debug.Log("Coin Collected");
	}

	public void OnAlert()
	{
		Debug.Log("Alert");
	}

	public void OnPlayerDeath()
	{
		Debug.Log("Player died");
	}

	public void OnDispawnClone()
	{
		Debug.Log("Clone Dispawn");
	}
}
