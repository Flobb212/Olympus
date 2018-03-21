using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public virtual void Spawn(Room parentRoom)
    {
        throw new System.NotImplementedException();
    }
}
