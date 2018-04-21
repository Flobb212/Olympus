using UnityEngine;
using System.Collections;

namespace Pathfinding
{
	/** Sets the destination of an AI to the position of a specified object.
	 * This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	 * This component will then make the AI move towards the #target set on this component.	
	 */
	[UniqueComponent(tag = "ai.destination")]
	
	public class AIDestinationSetter : VersionedMonoBehaviour
    {
		/** The object that the AI should move to */
		public Transform target;
		IAstarAI ai;

		void OnEnable ()
        {
            target = FindObjectOfType<PlayerCharacter>().transform;
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable ()
        {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/** Updates the AI's destination every frame */
		void Update ()
        {
			if (target != null && ai != null) ai.destination = target.position;
		}
	}
}
