using UnityEngine;
using System.Linq;

namespace Pathfinding
{
    /** Moves the target in example scenes.
	 * This is a simple script which has the sole purpose
	 * of moving the target point of agents in the example
	 * scenes for the A* Pathfinding Project.
	 *
	 * It is not meant to be pretty, but it does the job.
	 */
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
    public class TargetMover : MonoBehaviour
    {
        /** Mask for the raycast placement */
        public LayerMask mask;

        public Transform target;
        IAstarAI[] ais;

        public GameObject thisTarget;

        Camera cam;

        public void Start()
        {
            //Cache the Main Camera
            cam = Camera.main;
            // Slightly inefficient way of finding all AIs, but this is just an example script, so it doesn't matter much.
            // FindObjectsOfType does not support interfaces unfortunately.
            ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
            useGUILayout = false;
        }


        /** Update is called once per frame */
        void Update()
        {
            target.position = thisTarget.transform.position;

            for (int i = 0; i < ais.Length; i++)
            {
                if (ais[i] != null) ais[i].SearchPath();
            }
        }
    }
}