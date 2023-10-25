using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    public List<GameObject> gunMesh;
    public LayerMask hitmask;
    public Transform grenadePos;
    public MouseLook mouseLook;
    public Image gunIcon;
    public Text bulletStat;
    public Text grenadeText;
    public GameObject grenade;
    public GameObject scope;
    public GameObject weaponCam;
    public Button[] fireButton;

    private Gun gun;
    [SerializeField] private int gunIndex = -1;
    [SerializeField] private float grenadeThrowingForce = 10;
    private int nGreande = 2;
    private bool isReloading = false;
    private bool isAim = false;
    private float nextTimeToFire = 0;

    private const string reloadS = "RELOAD";
    private const string fireS = "FIRE";
    private const string aimS = "AIM";
    private const char slashS = '/';

    private void Start()
    {
        int index = PlayerPrefs.GetInt("WEAPON");
        ChangeGun(index);
        PlayerPrefs.SetInt("WEAPON", 0);
        grenadeText.text = nGreande.ToString();
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        if (gunIndex == -1 || isReloading) return;

        if ((fireButton[0].Pressed || fireButton[1].Pressed) && Time.time >= nextTimeToFire)
        {
            if (gun.bulletInMag <= 0) { StartReload(); return; }
            nextTimeToFire = Time.time + 1 / gun.fireRate;
            Shoot();
        }

    }

    public void StartReload()
    {
        if (gun.hasScope && isAim) SniperScope();
        if (gun.bulletInMag != gun.MagCapacity) StartCoroutine(Reload());
    }

    public void Scope()
    {
        if (gun.hasScope)  Invoke("SniperScope", 0.2f);
        isAim = !isAim; gun.anim.SetBool(aimS, isAim);
    }

    void SniperScope()
    {
        bool isActive = weaponCam.activeInHierarchy;
        scope.SetActive(isActive);
        weaponCam.SetActive(!isActive);
        if (isActive) Camera.main.fieldOfView = 10;
        else Camera.main.fieldOfView = 60;
    }

    public void ThrowGrenade()
    {
        if (nGreande <= 0) return;
        Rigidbody rb = Instantiate(grenade, grenadePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        if (rb != null) rb.AddForce(Camera.main.transform.forward * grenadeThrowingForce, ForceMode.VelocityChange);
        nGreande -= 1;
        grenadeText.text = nGreande.ToString();
    }    

    void Shoot()
    {
        if (gun.hasScope && isAim) { SniperScope(); Invoke("SniperScope", 0.2f);  }
        gun.gunFire.Play();
        gun.anim.SetTrigger(fireS);
        gun._muzzleFlash.Play();
        mouseLook.MoveCamera(gun.recoilX, gun.recoilY);
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit , hitmask))
        {
            Vector3 direction = (hit.point - gun.tip.position).normalized;
            Rigidbody rb = Instantiate(gun.lineofFire, gun.tip.position, transform.rotation).GetComponent<Rigidbody>();
            rb.velocity = direction * gun.bulletSpeed;
        }
        gun.bulletInMag -= 1;
        Time.timeScale = 1;
        bulletStat.text = gun.bulletInMag.ToString() + slashS + gun.TotalBullet;
    }

    IEnumerator Reload()
    {
        if (gun.TotalBullet <= 0 && isReloading) yield return null;
        
        isReloading = true;
        gun.reload.Play();
        gun.anim.SetTrigger(reloadS);
        yield return new WaitForSeconds(gun.reloadTime);
        if (gun.MagCapacity >= gun.TotalBullet)
        {
            gun.bulletInMag = gun.TotalBullet;
            gun.TotalBullet = 0;
        }
        else
        {
            gun.bulletInMag = gun.MagCapacity;
            gun.TotalBullet -= gun.MagCapacity;
        }
        bulletStat.text = gun.bulletInMag.ToString() + slashS + gun.TotalBullet;
        isReloading = false;
        if (isAim && gun.hasScope) SniperScope();
    }

    //Implement Gun Change
    void ChangeGun(int index)
    {
        if (gunIndex != -1) gunMesh[gunIndex].SetActive(false);
        gunIndex = index;
        if(gunIndex != -1)
        {
            gunMesh[gunIndex].SetActive(true);
            gun = gunMesh[gunIndex].GetComponent<Gun>();
            bulletStat.gameObject.SetActive(true);
            bulletStat.text = gun.bulletInMag.ToString() + slashS + gun.TotalBullet;
            gunIcon.sprite = gun.icon;
        }
    }
}
