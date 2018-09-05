using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public float speed = 20;

	public GameObject bulletPrefab;

	//총알을 스폰하는 인자 선언
	public Transform bulletSpawn;

	public override void OnStartLocalPlayer()
	{
		//내가 로컬플레이어 일때 실행되는 코드
		GetComponent<Renderer>().material.color = Color.blue;
	}

	// Update is called once per frame
	private void Update()
	{
		//거르는 역할
		if (!isLocalPlayer)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		//회전을 한다음 앞으로 가는 게임
		transform.Rotate(0, x * 20 * speed * Time.deltaTime, 0);

		transform.Translate(0, 0, z * speed * Time.deltaTime);
	}

	[Command]
	//[Command] 속성은 클라이언트에서 실행시키면, 실제 동작은 서버에서만 이루어짐.
	private void CmdFire()
	{
		//총알생성
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		//
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

		NetworkServer.Spawn(bullet);

		//2초취 사라짐.
		Destroy(bullet, 2.0f);
	}
}