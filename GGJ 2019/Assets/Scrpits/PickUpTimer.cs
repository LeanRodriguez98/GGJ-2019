using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpTimer : MonoBehaviour
{
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
    }

    private void Update()
    {
		Vector2 barPos = cam.WorldToScreenPoint(playerTransform.position);
		loadingBarTransform.position = new Vector2(barPos.x + offsetX, barPos.y + offsetY);

        if (currentObj != null)
        {
            if (!currentObj.beingPickedUp)
            {
                loadingBarImage.fillAmount = 0;
                loadingBarImage.gameObject.SetActive(false);
            }
        }
    }

    public void EnableLoadingCircle()
    {
        loadingBarImage.gameObject.SetActive(true);

        if (loadingBarImage.fillAmount < 1)
        {
            loadingBarImage.fillAmount += Time.deltaTime / currentObj.timeToPickUp;
        }
        else
        {
            Debug.Log("Object Picked Up!");
            currentObj.PickedUp();
            loadingBarImage.gameObject.SetActive(false);
        }
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

