using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem _muzzleFlash;
    public GameObject lineofFire;
    public Animator anim;
    public AudioSource reload;
    public Transform tip;
    public Sprite icon;
    public AudioSource gunFire;
    public float reloadTime = 2f;
    public float bulletSpeed = 50;
    public float fireRate = 15f;
    public float recoilX = .1f;
    public float recoilY = .2f;
    public int TotalBullet = 180;
    public int MagCapacity = 30;
    public int bulletInMag = 30;
    public bool hasScope = false;

}
