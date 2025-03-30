using System.Collections.Generic;
using UnityEngine;


public class Compass : MonoBehaviour
{
	private ObjectiveManager objectiveManager;

	[Header("Gui references")]
	[SerializeField] private RectTransform compassRect;
	[SerializeField] private GameObject pipsContainer;
	[SerializeField] private RectTransform closestObjectivePip;

	[Header("pip references")]
	[SerializeField] private RectTransform compassPipPrefab;
	private List<RectTransform> objectivePips = new List<RectTransform>();

    private Camera playerCamera;
    private Vector3 cameraPosition;

    private float compassBounds;

    private void Start()
    {
	objectiveManager = GetComponent<ObjectiveManager>();
	playerCamera = Camera.main;
	compassBounds = compassRect.rect.width * 0.5f;
    CreateObjectivePips();
    }

	private void Update()
	{
		cameraPosition = new Vector3(playerCamera.transform.position.x, 0, playerCamera.transform.position.z);

		if (objectivePips.Count < objectiveManager.FishSpots.Count) return;
		UpdatePips();
	}

	private float PlaceCompassPip(Vector3 target)
	{
		Vector3 VectorToTarget = (target - cameraPosition).normalized;
		float pipPostion = Vector3.Dot(playerCamera.transform.right, VectorToTarget);
		
		if (Vector3.Dot(playerCamera.transform.forward, VectorToTarget) <= 0)
		{
			if (pipPostion <= 0) pipPostion = -1f;
			if (pipPostion >= 0) pipPostion = 1f;
		}

		return compassBounds * pipPostion;
	}

	private void CreateObjectivePips()
	{
		foreach (RectTransform pip in objectivePips)
		{
			Destroy(pip.gameObject);
		}
		objectivePips.Clear();
		foreach (GameObject location in objectiveManager.FishSpots)
		{
			RectTransform instance = Instantiate(compassPipPrefab, pipsContainer.transform);
			objectivePips.Add(instance);
		}
	}

	private void UpdatePips()
	{
		int index = 0;
		for (int i = index; i < objectivePips.Count; i++)
		{
			Vector3 objectiveLocation = new Vector3(objectiveManager.FishSpots[i].transform.position.x, 0, objectiveManager.FishSpots[i].transform.position.z);
			objectivePips[i].localPosition = new Vector3(PlaceCompassPip(objectiveLocation), 0, 0);
		}

		// updating the closest objective constantly can lead to player confusion.
		// objectiveManager.UpdateClosestObjective();

		Vector3 closestObjectiveLocation = new Vector3(objectiveManager.closestObjective.transform.position.x, 0, objectiveManager.closestObjective.transform.position.z);
		closestObjectivePip.localPosition = new Vector3(PlaceCompassPip(closestObjectiveLocation), 0, 0);
	}

}
