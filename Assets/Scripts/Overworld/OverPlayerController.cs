using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OverPlayerController : MonoBehaviour {

    private float movementSpeed = 1.5f;
    public BattleControl battler;
    public Text aboveHeadText;
    private bool inCity = false;
    private CityInfo currentCity;
    private bool inMenu = false;
    // Use this for initialization
    void Start()
    {
        aboveHeadText.gameObject.transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("J_MainVertical"));
        //Debug.Log(InputManager.MainVertical());
        if (!battler.activeBattle && !battler.startingBattle)
        {

            if (Mathf.Abs(InputManager.MainHorizontal()) > 0.2f || Mathf.Abs(InputManager.MainVertical()) > 0.2f)
            {
                transform.position += InputManager.MainJoystick() * Time.deltaTime * movementSpeed;
                transform.rotation = Quaternion.LookRotation(InputManager.MainJoystick());
                float distanceTraveled = InputManager.MainJoystick().magnitude * Time.deltaTime * movementSpeed;
                battler.AddDistance(distanceTraveled);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("City")) {
            Debug.Log("Entering City");
            inCity = true;
            currentCity = other.gameObject.GetComponent<CityInfo>();
            aboveHeadText.text = currentCity.getName();
            aboveHeadText.gameObject.transform.parent.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("City"))
        {
            Debug.Log("Leaving City");
            inCity = false;
            currentCity = null;
            aboveHeadText.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
