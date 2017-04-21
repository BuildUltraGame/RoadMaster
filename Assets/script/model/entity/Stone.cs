using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : CollisionBaseHandler {

    public float rotateSpeed = 70f;

    private GameObject spawnerGo;

    public void setSpawner(GameObject go)
    {
        spawnerGo = go;
    }

    public override void OnWorldUnitCollisionStart(Collider worldUnit)
    {
        collision(worldUnit);
    }

    public override void OnEnemyCollisionStart(Collider enemy)
    {
        collision(enemy);
    }

    public override void OnSelfUnitCollisionStart(Collider selfUnit)
    {
        collision(selfUnit);
    }

    void Update()
    {
        transform.Rotate(transform.forward * Time.deltaTime* rotateSpeed);
    }

    private void collision(Collider unit)
    {
        if (unit.gameObject.Equals(spawnerGo))
        {
            return;
        }
        if (unit.gameObject.layer == Layers.BUILDING || unit.tag == Tags.GATE)
        {
            //碰到建筑自身销毁

            Destroy(gameObject, 0.5f);
        }
        else
        {
            if (unit.gameObject.layer == Layers.VEHICLE || unit.gameObject.layer == Layers.CHARACTER)
            {
                Destroy(unit.gameObject, 0.3f);
            }

        }
        
    }

}
