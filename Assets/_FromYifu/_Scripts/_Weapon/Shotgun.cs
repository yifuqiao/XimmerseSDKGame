using UnityEngine;
using System.Collections;
using System;

public class Shotgun : WeaponBase
{
    private float m_accuTime = 0f;
    private bool m_bCanFire = true;
    private bool m_bIsReloading = false;
    private AudioSource m_audioSource;
    private Animator m_animator;
    [SerializeField]
    private Light m_fireEffect;

    private void Awake()
    {
        m_fireEffect.enabled = false;
        m_curAmmoCount = m_maxAmmo;
        m_audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
    }

    public override bool CanFire()
    {
        return m_bCanFire;
    }

    public override void Fire()
    {
        if (m_bCanFire == false || m_curAmmoCount<=0)
            return;
        --m_curAmmoCount;
        StartCoroutine(DoFireEffect());
    }

    private IEnumerator DoFireEffect()
    {
        m_audioSource.PlayOneShot(m_sfClip);
        m_bCanFire = false;
        m_fireEffect.enabled = true;
        yield return new WaitForSeconds(0.1f);
        m_fireEffect.enabled = false;
        yield return new WaitForSeconds(m_fireInterval - 0.1f);
        m_bCanFire = true;
    }

    public override void Reload()
    {
        m_bIsReloading = true;
        m_accuTime = 0f;
        m_animator.SetBool("Reload", true);
        m_audioSource.PlayOneShot(m_sfReloadClip);
    }
}
