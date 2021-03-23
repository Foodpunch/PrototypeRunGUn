using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WeaponManager instance;
    //public BaseGun gun;
   // public GunStats gun;
    BaseGun[] gunArray;
    public int currentGun = 0;
    [SerializeField] Text ammoUI;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gunArray = gameObject.GetComponents<BaseGun>();
        for(int i=0; i<gunArray.Length;i++)
        {
            gunArray[i].enabled = false;
        }
        gunArray[0].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ammoUI.text = gunArray[currentGun].currentAmmo.ToString();
    }
    public void ChangeWeapon()  //testing script to test weapons and gameplay
    {
        currentGun++;
        if (currentGun >= gunArray.Length)
        {
            currentGun = 0;
        }
        for(int i=0; i<gunArray.Length; i++)
        {
            gunArray[i].enabled = false;
        }
        gunArray[currentGun].enabled = true;
    }
}
