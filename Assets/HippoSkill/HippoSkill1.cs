using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

public class HippoSkill1 : ASkill
{
    [SerializeField] public Transform start;
    [SerializeField] public float skillRadius = 20;
    [SerializeField] public SimpleRoket roket;

    private List<SimpleRoket> listAmmo = new List<SimpleRoket>();
    public override void Ability(SceneData sceneData)
    {
        foreach (GameObject go in sceneData.Enemies)
        {
            Vector3 diff = go.transform.position - start.position;
            if (diff.magnitude < skillRadius)
            {
                SimpleRoket _bullet = Instantiate(roket, start.position, start.rotation);
                _bullet.Target = go.transform;
                listAmmo.Add(_bullet);
            }
        }
        
    }
    public override void Init()
    {
        nameKey = "Ability1";
        //StartCoroutine("UpdateBullet");
    }
    private IEnumerator UpdateBullet()
    {
        while (true)
        {
            foreach (SimpleRoket go in listAmmo)
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
        }
    }
    public override void SkillUpdate(SceneData sceneData)
    {
        foreach (SimpleRoket go in listAmmo)
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
