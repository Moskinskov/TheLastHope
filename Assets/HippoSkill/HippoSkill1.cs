using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

public class HippoSkill1 : ASkill
{
    [SerializeField] public Transform start;
    [SerializeField] public float skillRadius = 20;
    [SerializeField] public SimpleRocket rocket;

    private List<SimpleRocket> listAmmo = new List<SimpleRocket>();
    public override void Ability(SceneData sceneData)
    {
        foreach (GameObject go in sceneData.Enemies)
        {
            Vector3 diff = go.transform.position - start.position;
            if (diff.magnitude < skillRadius)
            {
                SimpleRocket _bullet = Instantiate(rocket, start.position, start.rotation);
                _bullet.Target = go.transform;
                listAmmo.Add(_bullet);
            }
        }
        
    }
    public override void Init()
    {
        nameKey = "Ability1";

    }
    //private IEnumerator UpdateBullet()
    //{
    //    while (true)
    //    {
    //        foreach (SimpleRocket go in listAmmo)
    //        {
    //            print(go);
    //            if (go == null)
    //            {
    //                listAmmo.Remove(go);
    //            }
    //            else
    //            {
    //                go.UpdateBullet(Time.deltaTime);
    //            }
    //        }
    //    }
    //}
    public override void SkillUpdate(SceneData sceneData)
    {
        foreach (SimpleRocket go in listAmmo)
        {
            print(go);
            if (go == null)
            {
                listAmmo.Remove(go);
            }
            else
            {
                go.UpdateBullet(Time.deltaTime);
            }
        }
        print("End.");
        base.SkillUpdate(sceneData);
    }
}
