using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
	public bool isEnemy;
	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public Slider healthSlider;

	public void TakeDamage(int amount)
	{
		if (!isServer)
			return;

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			if(isEnemy)
			{
				Destroy(gameObject);
			}
			else
			{
				currentHealth = maxHealth;
				RpcRespawn();
				Debug.Log("Dead!");
			}
			
		}
	}

	private void OnChangeHealth(int health)
	{
		currentHealth = health;
		healthSlider.value = health;
	}

	// 방장이 실행시키면, 모든 손님들도 다 따라서 실행함
	[ClientRpc]
	private void RpcRespawn()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		transform.position = NetworkManager.singleton.GetStartPosition().position;
	}
}