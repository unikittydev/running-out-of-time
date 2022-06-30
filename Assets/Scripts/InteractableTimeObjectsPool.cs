using System.Linq;
using UnityEngine;

public class InteractableTimeObjectsPool : MonoBehaviour
{
    private static InteractableTimeObject[] _objects;

	private void Awake() =>
		_objects = FindObjectsOfType<InteractableTimeObject>(true);

	public static InteractableTimeObject[] GetClosestObjects(Vector2 point, float distance) =>
		_objects.Where(obj => Vector2.Distance(obj.transform.position, point) <= distance).ToArray();
	
}
