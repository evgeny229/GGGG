using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TornadoMove : MonoBehaviour
{
    public int Type;// 1-tornado 2- sand
    public int SizeX;
    public int SizeY;
    public float speed;
    int LastPosX;
    int LastPosZ;
    float speedRotate;
    Vector3 PosToMove;

    Island currIsland;


    public void SetParameters(int SizeX,int SizeY, int type,float speed, float speedRotate =0)
    {
        this.Type = type;
        this.SizeX = SizeX;
        this.SizeY = SizeY;
        this.speed = speed;
        this.speedRotate = speedRotate;

        transform.position = new Vector3(Random.Range(0, SizeX) * ObjectsOnScene.currGameLevelConfig._deltaIslandPosition+ ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.x, 0, Random.Range(0, SizeY)  * ObjectsOnScene.currGameLevelConfig._deltaIslandPosition + ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.z);
        ChangePosToMove();
       // ObjectsOnScene.currGameLevelConfig._deltaIslandPosition; - distance betwen islands
    }
    float timeKp = 0;
    float speedChangeTimeKp = 0.25f;
    public void Move()
    {
        timeKp += Time.deltaTime * speedChangeTimeKp;
        float distKp = 1f;
        if (Vector3.Distance(transform.position, PosToMove) < 100f)
        {
            distKp = Vector3.Distance(transform.position, PosToMove) / 100f;
            if (distKp < 0.2f)
                distKp = 0.2f;
        }
        transform.position += (PosToMove - transform.position).normalized * speed * Time.deltaTime * timeKp * distKp;
        if (Type == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 0.3f) * 0.25f, transform.position.z);
        }
        if (Type == 1)
        {
            transform.Rotate(new Vector3(0, speedRotate * Time.deltaTime, 0));
        }
        else
        {
            Vector3 direction = PosToMove - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, 0.003f, 500f));
        }
        CheckCurrentPosition();
        if (Vector3.Distance(transform.position, PosToMove) < 1f)
            ChangePosToMove();       
    }
    public void ChangePosToMove()
    {
        timeKp = 0;
        PosToMove = new Vector3(Random.Range(0, SizeX)  * ObjectsOnScene.currGameLevelConfig._deltaIslandPosition+ ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.x, 0, Random.Range(0, SizeY)  * ObjectsOnScene.currGameLevelConfig._deltaIslandPosition+ ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.z);
    }
    void OnChangeIsland(Island newIsland)
    {
        if(currIsland != null) 
        {
        currIsland.SetActionEvent(0);
        }
        newIsland.SetActionEvent(Type);
        currIsland = newIsland;
    }
    void CheckCurrentPosition()
    {     
       int posX = (int)((transform.position.x + ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.x) / ObjectsOnScene.currGameLevelConfig._deltaIslandPosition);
       int posZ = (int)((transform.position.z + ObjectsOnScene.currGameLevelConfig._FirstIslandPosition.z) / ObjectsOnScene.currGameLevelConfig._deltaIslandPosition);
        if(posX != LastPosX || posZ != LastPosZ)
        {
            OnChangeIsland(ObjectsOnScene.currMapController._currIslands[posX, posZ]);
        }
        else if (currIsland != null)
        {
                currIsland.SetActionEvent(Type);
        }
        LastPosX = posX;
        LastPosZ = posZ;
    }
}
