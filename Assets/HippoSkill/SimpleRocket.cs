using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;

public class SimpleRocket : AAmmo
{
    private Transform target;

    public Transform Target { get => target; set => target = value; }

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
            Die(true);
        }
        catch { };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((!collision.gameObject.GetComponent<AEnemy>()) && (!collision.gameObject.GetComponent<AAmmo>())) Die(false);
    }
    public void UpdateBullet()
    {
        if (target != null)
        {
            Vector3 dir = Target.position - transform.position;
            transform.position += dir.normalized * Speed;
        }
        else
        {
            Die(true);
        }
    }
    private void Die(bool withSnd)
    {
        var _explosion = this.gameObject.transform.GetChild(0);
        _explosion.gameObject.SetActive(true);
        var snd = this.GetComponent<AudioSource>();
        this.gameObject.GetComponent<Renderer>().enabled = false;
        if (snd)
        {
            if (withSnd) snd.Play();
        }
        Destroy(this.gameObject, 0.3f);
    }
}
