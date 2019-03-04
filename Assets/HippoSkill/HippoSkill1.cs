using System.Collections;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

public class HippoSkill1 : ASkill
{
    [SerializeField] public Transform start;
    [SerializeField] public float skillRadius = 20;
    [SerializeField] public AAmmo rocket;

    private List<AAmmo> listAmmo = new List<AAmmo>();
    public override void Ability(SceneData sceneData)
    {
        foreach (GameObject go in sceneData.Enemies)
        {
            AAmmo _bullet = Instantiate(rocket, start.position, start.rotation);
            //_bullet.Target = go.transform;
            listAmmo.Add(_bullet);
        }
        
    }
    public override void Init()
    {
        nameKey = "Ability1";
        //StartCoroutine("UpdateBullet");
    }
    //private IEnumerator UpdateBullet()
    //{
    //    while (true)
    //    {
    //        print("Kek");
    //        foreach (AAmmo go in listAmmo)
    //        {
    //            print(go);
    //            if (go == null)
    //            {
    //                listAmmo.Remove(go);
    //            }
    //            else
    //            {
    //                go.UpdateBullet();
    //            }
    //        }
    //        print("end.");
    //    }
    //}
    public override void SkillUpdate(SceneData sceneData)
    {
        base.SkillUpdate(sceneData);
        
    }
}
