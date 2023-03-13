using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EventController : MonoBehaviour
{
    List<TornadoMove> tornados;
    public void Awake()
    {
        ObjectsOnScene.currEventController = this;
        tornados = new List<TornadoMove>();
    }
    public void SetParameters(int eventType, int SizeX, int SizeY )
    {
        int count = 0;
        switch (eventType)
        {
            case 1:  //tornado
                count = 1 + SizeX * SizeY / 10;
                for (int i = 0; i < count; i++)
                {
                tornados.Add(GenerateEventObject(ObjectsOnScene.currAllObjects._eventObjects[0], SizeX, SizeY,1,35f, 200f));
                }
                break;
            case 2:  //sand
                count = 1 + SizeX * SizeY / 3;
                for (int i = 0; i < count; i++)
                    tornados.Add(GenerateEventObject(ObjectsOnScene.currAllObjects._eventObjects[1], SizeX, SizeY, 2, 15f, 100f));
                break;
            case 3:  //snow
                count = 1 + SizeX * SizeY / 2;
                for (int i = 0; i < count; i++)
                    tornados.Add(GenerateEventObject(ObjectsOnScene.currAllObjects._eventObjects[2], SizeX, SizeY, 3, 10f, 100f));
                break;
            case 4:  //rain
                count = 1 + SizeX * SizeY / 5;
                for (int i = 0; i < count; i++)
                    tornados.Add(GenerateEventObject(ObjectsOnScene.currAllObjects._eventObjects[3], SizeX, SizeY, 4, 25f, 50f));
                break;
        }
        Debug.Log($"EventType:{eventType} ");
    }
    public TornadoMove GenerateEventObject(GameObject prefabEvent,int SizeX , int SizeY,int type,float speed, float speedRotate = 0)
    {
      GameObject g = Instantiate(prefabEvent, ObjectsOnScene.currBridges);
        //active prefab
        g.GetComponent<TornadoMove>().SetParameters(SizeX,SizeY,type,speed, speedRotate);
        return g.GetComponent<TornadoMove>();
    }
    private void Update()
    {
       foreach(TornadoMove tornado in tornados)
        {
            tornado.Move();
        }
    }

}
