using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;

public class BulletController : AAmmo {
	private Transform bulletItem;

	// Use this for initialization
	void Start () {
		bulletItem = transform;
	}
	
	// Update is called once per frame
	void Update () {
		bulletItem.position += bulletItem.forward * speed * Time.deltaTime;
	}

	public override void OnTriggerEnter(Collider col) {
        try { col.GetComponent<AEnemy>().SetDamage(damage); }
        catch { }
        Destroy(gameObject);
	}

	public override void OnDepopulate()
	{
		throw new System.NotImplementedException();
	}

	public override void OnPopulate()
	{
		throw new System.NotImplementedException();
	}

	//public override void Move(float deltaTime)
	//{
	//    throw new System.NotImplementedException();
	//}
}
