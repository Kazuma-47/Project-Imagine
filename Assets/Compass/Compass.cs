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
	public RectTransform compassPipPrefab;
	public GameObject pipsContainer;
	public List<RectTransform> objectivePips = new List<RectTransform>();

	public Camera playerCamera;
	public GameObject objective;
	private Vector2 playerPosition;

	public RectTransform closestObjectivePip;
	// public RectTransform objectivePip;

	private GameObject compass;
	public RectTransform compassRect;
	private float compassPipPosition;
	private float compassBounds;
	


    void Start()
    {
        compass = transform.parent.gameObject;
		compassBounds = compassRect.rect.width * 0.5f;
    }

    void Update()
    {
		playerPosition = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.z);

		if (Input.GetKeyDown("space"))
		{
			CreateObjectivePips();
		}

		if (objectivePips.Count < objectiveManager.objectives.Count) return;
		UpdatePips();
    }

	private float PlaceCompassPip(Vector2 target)
	{
		Vector2 VectorToTarget = (target - playerPosition).normalized;
		float pipPostion = Vector2.Dot(playerCamera.transform.right, VectorToTarget);
		
		if (Vector2.Dot(playerCamera.transform.up, VectorToTarget) <= 0)
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
			Vector2 objectiveLocation = new Vector2(objectiveManager.objectives[i].transform.position.x, objectiveManager.objectives[i].transform.position.z);
			objectivePips[i].localPosition = new Vector3(PlaceCompassPip(objectiveLocation), 0, 0);
		}

		// updating the closest objective constantly can lead to player confusion.
		// objectiveManager.UpdateClosestObjective();

		Vector2 closestObjectiveLocation = new Vector2(objectiveManager.closestObjective.transform.position.x, objectiveManager.closestObjective.transform.position.z);
		closestObjectivePip.localPosition = new Vector3(PlaceCompassPip(closestObjectiveLocation), 0, 0);
	}

}
