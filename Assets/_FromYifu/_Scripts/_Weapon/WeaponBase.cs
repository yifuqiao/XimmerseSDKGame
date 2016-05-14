using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour {

    [SerializeField]
    protected float m_fireInterval;
    [SerializeField]
    protected int m_maxAmmo;
    [SerializeField]
    protected float m_reloadingTime;
    [SerializeField]
    protected AudioClip m_sfClip;
    [SerializeField]
    protected AudioClip m_sfReloadClip;


    protected int m_curAmmoCount;
    public abstract void Fire();
    public abstract void Reload();
    public abstract bool CanFire();
}
