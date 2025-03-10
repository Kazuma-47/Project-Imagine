using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class Compass : MonoBehaviour
{
	public ObjectiveManager objectiveManager;

	//camera references
	public GameObject playerCamera;
	private Vector3 cameraPosition;

	//GUI references
	public RectTransform compassRect;
	private float compassBounds;
	public GameObject pipsContainer;
	public RectTransform closestObjectivePip;

	//pips
	public RectTransform compassPipPrefab;
	private List<RectTransform> objectivePips = new List<RectTransform>();

	//debug
	public GameObject debug1;
	public GameObject debug2;

    void Start()
    {
		compassBounds = compassRect.rect.width * 0.5f;
    }

    void Update()
    {
		cameraPosition = new Vector3(playerCamera.transform.position.x, 0, playerCamera.transform.position.z);

		if (Input.GetKeyDown("space"))
		{
			CreateObjectivePips();
		}

		if (objectivePips.Count < objectiveManager.objectives.Count) return;
		UpdatePips();
    }

	private float PlaceCompassPip(Vector3 target)
	{
		Vector3 VectorToTarget = (target - cameraPosition).normalized;
		float pipPostion = Vector3.Dot(playerCamera.transform.right, VectorToTarget);

		debug1.transform.position = playerCamera.transform.position + playerCamera.transform.right;
		debug2.transform.position = playerCamera.transform.position + new Vector3(VectorToTarget.x, 0, VectorToTarget.y);
		
		if (Vector3.Dot(playerCamera.transform.forward, VectorToTarget) <= 0)
		{
			if (pipPostion <= 0) pipPostion = -1f;
			if (pipPostion >= 0) pipPostion = 1f;
		}

		return compassBounds * pipPostion;
	}

	void CreateObjectivePips()
	{
		// updating the closest objective constantly can lead to player confusion. thats why it only happens here
		objectiveManager.UpdateClosestObjective();

		foreach (RectTransform pip in objectivePips)
		{
			Destroy(pip.gameObject);
		}
		objectivePips.Clear();
		foreach (GameObject location in objectiveManager.objectives)
		{
			RectTransform instance = Instantiate(compassPipPrefab, pipsContainer.transform);
			objectivePips.Add(instance);
		}
	}

	void UpdatePips()
	{
		int index = 0;
		for (int i = index; i < objectivePips.Count; i++)
		{
			Vector3 objectiveLocation = new Vector3(objectiveManager.objectives[i].transform.position.x, 0, objectiveManager.objectives[i].transform.position.z);
			objectivePips[i].localPosition = new Vector3(PlaceCompassPip(objectiveLocation), 0, 0);
		}

		// updating the closest objective constantly can lead to player confusion.
		// objectiveManager.UpdateClosestObjective();

		Vector3 closestObjectiveLocation = new Vector3(objectiveManager.closestObjective.transform.position.x, 0, objectiveManager.closestObjective.transform.position.z);
		closestObjectivePip.localPosition = new Vector3(PlaceCompassPip(closestObjectiveLocation), 0, 0);
	}

}
