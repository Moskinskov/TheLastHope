using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;

public class SimpleBullet : AAmmo
{
	[SerializeField] protected float _lifeTime = 2.0f; 
	public void Awake() //Временно! Убрать при реализации ObjectPool'а.
	{
		//Destroy(this, _lifeTime);
	}

	public override void OnPopulate()
	{
		throw new System.NotImplementedException();
	}

	public override void OnDepopulate()
	{
		throw new System.NotImplementedException();
	}

	public override void OnTriggerEnter(Collider collision)
	{
		try
		{
			collision.gameObject.GetComponent<AEnemy>().SetDamage(damage);
			var snd = this.GetComponent<AudioSource>();
			if (snd != null)
			{
				//print("Not NUll!");
				snd.Play();
			}
		}
		catch { };

		Destroy(this.gameObject, 0.2f);
	}
}
