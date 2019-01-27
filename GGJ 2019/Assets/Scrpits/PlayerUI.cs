using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	public GameObject objectInfo;
	public Text objectName;
	public Text happinessValue; // later define if value changes between players
	public Text weight;
	public Text timeToPickUp;


	public Transform playerTransform;
    public Image loadingBarImage;
	public float offsetX;
	public float offsetY;

	private Camera cam;
	private Transform loadingBarTransform;
	private float pickUpTime;
    private bool startTimer;

    private PickUpableObject currentObj;
    public void SetCurrentObj(PickUpableObject obj) { currentObj = obj; }

    private void Start()
    {
		cam = Camera.main;
		loadingBarTransform = loadingBarImage.gameObject.transform;
        loadingBarImage.fillAmount = 0;
        pickUpTime = 0;
        startTimer = false;

        loadingBarImage.gameObject.SetActive(false);
		objectInfo.SetActive(false);
    }

    private void Update()
    {
		Vector2 barPos = cam.WorldToScreenPoint(playerTransform.position);
		loadingBarTransform.position = new Vector2(barPos.x + offsetX, barPos.y + offsetY);

        if (currentObj != null)
        {
            if (!currentObj.BeingPickedUp)
            {
                //loadingBarImage.fillAmount = 0;
                //loadingBarImage.gameObject.SetActive(false);
            }
        }
    }

    public void EnableLoadingCircle()
    {
        loadingBarImage.gameObject.SetActive(true);
        Debug.Log("culo encendedor");

        if (loadingBarImage.fillAmount < 1)
        {
            Debug.Log("Picking up object!");
            loadingBarImage.fillAmount += Time.deltaTime / currentObj.timeToPickUp;
        }
        else
        {
            Debug.Log("Object Picked Up!");
            currentObj.PickedUp();
            loadingBarImage.gameObject.SetActive(false);
        }
    }

	public void DisplayInfo()
	{
		if (currentObj != null)
		{
			objectName.text = "Object: " + currentObj.gameObject.name;
			happinessValue.text = "Happiness: " + currentObj.happiness;
			weight.text = "Weight: " + currentObj.weight;
			timeToPickUp.text = "Time to pick up: " + currentObj.weight;
			objectInfo.SetActive(true);
		}
	}

	public void HideInfo()
	{
		objectInfo.SetActive(false);
	}

	public void StartTimer()
    {
        startTimer = true;
    }

    public void ResetTimer()
    {
        loadingBarImage.fillAmount = 0;
    }
}

