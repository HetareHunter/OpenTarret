using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Managers;
using UniRx;

public class BaseTarretAttack : MonoBehaviour
{
    [SerializeField] TarretData tarretData;
    [SerializeField] GameObject muzzleFrameJoint;

    [SerializeField] float rayDistance = 1;
    [SerializeField] GameObject rayOfOrigin;

    [SerializeField] GameObject m_razerEffect;
    [SerializeField] GameObject m_wasteHeatEffect;
    [SerializeField] GameObject m_shockWaveEffect;
    [SerializeField] GameObject m_hitExplodeEffect;

    [SerializeField] GameObject m_razerEffectInsPosi;
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    [SerializeField] GameObject m_shockWaveEffectInsPosi;

    [SerializeField] float razerExistTime = 0.5f;
    [SerializeField] float wasteHeatExistTime = 2.0f;

    public bool attackable = true;

    GameObject m_razer;
    GameObject m_wasteHeat;
    GameObject m_shockWave;


    [SerializeField] GameObject muzzle;
    float muzzleRadius;
    AudioPlayer muzzleAudio;

    BaseTarretBrain baseTarretBrain;

    [SerializeField] GameObject magazine;
    MagazineRotate magazineRotate;
    [SerializeField] float untilRotateMagazine = 0.3f;

    [SerializeField] GameObject explodeobj;

    [SerializeField] GameObject sight;
    SightChanger sightChanger;
    [SerializeField] GameObject sightLeftSlider;
    [SerializeField] GameObject sightRightSlider;
    TarretScreenSliderChanger tarretScreenLeftSliderChanger;
    TarretScreenSliderChanger tarretScreenRightSliderChanger;
    bool screenColorRed = false;

    AttackIntervalCounter attackInterval;

    //　当たったコライダを入れておく変数
    RaycastHit[] m_hits;

    private void Start()
    {
        baseTarretBrain = GetComponent<BaseTarretBrain>();
        //　弾の半径を取得
        muzzleRadius = muzzle.GetComponent<SphereCollider>().radius;
        muzzleAudio = muzzle.GetComponent<AudioPlayer>();
        magazineRotate = magazine.GetComponent<MagazineRotate>();
        sightChanger = sight.GetComponent<SightChanger>();
        tarretScreenLeftSliderChanger = sightLeftSlider.GetComponent<TarretScreenSliderChanger>();
        tarretScreenRightSliderChanger = sightRightSlider.GetComponent<TarretScreenSliderChanger>();
        attackInterval = GetComponent<AttackIntervalCounter>();
    }

    void FixedUpdate()
    {
        RaySearchObject();
        //Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * rayDistance);
    }

    void RaySearchObject()
    {
        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        //　Sphereの形でレイを飛ばしEnemyレイヤーのものをhitsに入れる
        m_hits = Physics.SphereCastAll(ray, muzzleRadius, rayDistance, LayerMask.GetMask("Enemy"));
        if (m_hits.Length > 0)
        {
            if (screenColorRed == false)
            {
                sightChanger.ChangeRedTex();
                tarretScreenLeftSliderChanger.ChangeSliderFillRed();
                tarretScreenRightSliderChanger.ChangeSliderFillRed();
                screenColorRed = true;
            }
        }
        else
        {
            if (screenColorRed == true)
            {
                sightChanger.ChangeBaseTex();
                tarretScreenLeftSliderChanger.ChangeSliderFillBase();
                tarretScreenRightSliderChanger.ChangeSliderFillBase();
                screenColorRed = false;
            }
        }
    }

    void KillEnemyFromRazer()
    {

        foreach (var hit in m_hits)
        {

            explosionForce(hit.point);
            Instantiate(m_hitExplodeEffect, hit.point,Quaternion.identity);
            EnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<EnemyDeath>();
            enemyDeath.OnDead();
            //hit.collider.enabled = false;
            //Destroy(hit.collider.gameObject);
        }
    }

    //レーザーのライン部分のスクリプト
    void FireEffectManager()
    {

        m_razer = Instantiate(m_razerEffect, m_razerEffectInsPosi.transform.position, m_razerEffectInsPosi.transform.rotation);
        //LineRenderer razerLineRenderer = m_razer.transform.GetChild(2).gameObject.GetComponent<LineRenderer>();
        FadeFire();

    }

    void FadeFire()
    {
        //Debug.Log("終わり!");

        Destroy(m_razer, razerExistTime);
    }

    //ここまでレーザーのライン部分のスクリプト

    /// <summary>
    /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
    /// </summary>
    void WasteHeatEffectManager()
    {
        m_wasteHeat = Instantiate(m_wasteHeatEffect, m_wasteHeatEffectInsPosi.transform.position,
            m_wasteHeatEffectInsPosi.transform.rotation, m_wasteHeatEffectInsPosi.transform);
        Destroy(m_wasteHeat, wasteHeatExistTime);
    }

    void ShockWaveManager()
    {
        m_shockWave = Instantiate(m_shockWaveEffect, m_shockWaveEffectInsPosi.transform.position,
            m_shockWaveEffectInsPosi.transform.rotation);

        Destroy(m_shockWave, tarretData.shockWaveExistTime);
    }

    void explosionForce(Vector3 hitPosi)
    {
        GameObject explode = Instantiate(explodeobj, hitPosi , Quaternion.identity);
        BeamPower beamPower = explode.GetComponent<BeamPower>();
        beamPower.Movement(muzzleFrameJoint.transform.forward);
    }

    


    /// <summary>
    /// 攻撃したとき、TarretCommandステートがAttackになったときに一度だけ呼ばれるメソッド。
    /// </summary>
    public void BeginAttack()
    {
        muzzleAudio.AudioPlay();
        FireEffectManager();
        WasteHeatEffectManager();
        ShockWaveManager();
        KillEnemyFromRazer();
        Observable.Timer(TimeSpan.FromSeconds(untilRotateMagazine))
            .Subscribe(_ => magazineRotate.RotateMagazine());

        attackable = false;
        attackInterval.countStart = true;
        StayAttack();
    }

    void StayAttack()
    {
    }

    public void EndAttack()
    {
        attackable = true;
    }


#if UNITY_EDITOR

    private void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            if (baseTarretBrain.tarretCommandState != TarretCommand.Attack)
            {
                baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
            }
        }

    }
#endif
}