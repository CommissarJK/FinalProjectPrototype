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
    private List<CityInfo> allCities;
    private bool inMenu = false;
    public GameObject CityMenu;
    public Text LevelUI;
    public Text ExpUI;
    public Text ExpToLevelUI;
    public Text GoldUI;
    public Text GearUI;
    public Text UpgradeCostUI;

    private int gold = 0;
    public GameObject character;
    public GameObject ship;
    public GameObject GrassArena;
    public GameObject BoatArena;

    // Use this for initialization
    void Start()
    {
        aboveHeadText.gameObject.transform.parent.gameObject.SetActive(false);
        allCities = new List<CityInfo>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("J_MainVertical"));
        //Debug.Log(InputManager.MainVertical());
        int level = battler.getPlayerlevel();
        LevelUI.text = "Party Level: " + level;
        ExpUI.text = "Expereince: " + battler.getPlayerEXP();
        ExpToLevelUI.text = "Exp to level: " + ((level*level)*100);
        GoldUI.text = "Gold: " + gold;
        GearUI.text = "Gear Level: " + battler.getPlayerGear();
        if (!battler.activeBattle && !battler.startingBattle && !inMenu)
        {

            if (Mathf.Abs(InputManager.MainHorizontal()) > 0.2f || Mathf.Abs(InputManager.MainVertical()) > 0.2f)
            {
                transform.position += InputManager.MainJoystick() * Time.deltaTime * movementSpeed;
                transform.rotation = Quaternion.LookRotation(InputManager.MainJoystick());
                float distanceTraveled = InputManager.MainJoystick().magnitude * Time.deltaTime * movementSpeed;
                battler.AddDistance(distanceTraveled);
            }
            if (inCity) {
                if (InputManager.Abutton()) {
                    inMenu = true;
                    CityMenu.SetActive(true);
                }
            }
        }
        if (inMenu)
        {
            int upgradeCost = 200 + (200 * battler.getPlayerGear());
            //UpgradeCostUI.text = "Upgrade Gear: "+upgradeCost+"g";
            aboveHeadText.gameObject.transform.parent.gameObject.SetActive(false);
            if (InputManager.Bbutton())
            {
                inMenu = false;
                CityMenu.SetActive(false);
            }
            if (InputManager.Ybutton())
            {
                if (gold >= 50)
                {
                    battler.levelUp();
                    gold -= 50;
                }

            }
            if (InputManager.Xbutton())
            {
                if (gold >= upgradeCost && battler.getPlayerGear() < currentCity.getLevel())
                {
                    battler.upgradeGear();
                    gold -= upgradeCost;
                }
            }
        }
        else {
            CityMenu.SetActive(false);
            if (inCity)
            {
                aboveHeadText.gameObject.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    public void setStartPos(Vector3 pos) {
        gameObject.transform.position = pos + new Vector3(0, 1, 0);
    }

    public void addGold(int _gold) {
        this.gold += _gold;
    }

    public void addCity(CityInfo city) {
        allCities.Add(city);
        //Debug.Log(allCities.Count);
    }

    public void addExpToCity(int exp) {
        getClosestCity().addExp(exp);
    }

    public CityInfo getClosestCity()
    {
        CityInfo closest = allCities[0];
        float dist = Vector3.Distance(gameObject.transform.position, allCities[0].gameObject.transform.position);

        for (int i = 0; i < allCities.Count; i++)
        {
            float tempDist = Vector3.Distance(gameObject.transform.position, allCities[i].gameObject.transform.position);
            if (tempDist < dist)
            {
                closest = allCities[i];
                dist = tempDist;
            }
        }
        return closest;
    }

    void OnCollisionEnter(Collision other)
    {
  
        if (other.gameObject.transform.parent.gameObject.CompareTag("Hex"))
        {
            float f = other.gameObject.transform.parent.gameObject.GetComponent<HexComponent>().hex.elevation;
            if (f >= 0f)
            {
                character.SetActive(true);
                ship.SetActive(false);
                GrassArena.SetActive(true);
                BoatArena.SetActive(false);
            }
            else {
                ship.SetActive(true);
                character.SetActive(false);
                BoatArena.SetActive(true);
                GrassArena.SetActive(false);
                
            }
        }
        Debug.Log("here");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("City")) {
            Debug.Log("Entering City");
            inCity = true;
            currentCity = other.gameObject.GetComponent<CityInfo>();
            aboveHeadText.text = currentCity.getName()+" Lv. " +currentCity.getLevel();
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
