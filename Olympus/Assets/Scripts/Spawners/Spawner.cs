using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawner
{
    public virtual void Spawn(GameObject parentRoom)
    {
        throw new System.NotImplementedException();
    }
}
