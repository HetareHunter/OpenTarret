using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Managers;
using UniRx;
using DG.Tweening;

public class BaseTarretAttack : MonoBehaviour
{
    [SerializeField] TarretData tarretData;
    [SerializeField] GameObject muzzleFrameJoint;

    [SerializeField] Vector3 rayDistance;
    [SerializeField] GameObject rayOfOrigin;

    [SerializeField] GameObject m_razerEffect;
    //[SerializeField] GameObject m_razerSteamEffect;

    [SerializeField] GameObject m_wasteHeatEffect;
    [SerializeField] GameObject m_shockWaveEffect;
    [SerializeField] GameObject m_hitExplodeEffect;

    [SerializeField] GameObject m_razerEffectInsPosi;
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    [SerializeField] GameObject m_shockWaveEffectInsPosi;

    [SerializeField] float razerExistTime = 0.5f;
    //[SerializeField] float razerSteamExistTime = 0.5f;

    [SerializeField] float wasteHeatExistTime = 2.0f;

    public bool attackable = true;

    GameObject m_wasteHeat;
    GameObject m_shockWave;

    [SerializeField] AnimationCurve razerFadeAnim;
    LineRenderer razerLineRenderer;

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
    RaycastHit[] m_hitsEnemy;
    RaycastHit m_hitGameStart;

    public Vector3 rayHitFirstPosi;

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
        razerLineRenderer = m_razerEffect.transform.GetChild(0).GetComponent<LineRenderer>();
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
        m_hitsEnemy = Physics.SphereCastAll(ray, muzzleRadius, rayDistance.z, LayerMask.GetMask("Enemy", "GameManage"));
        //m_hitGameStart = Physics.Raycast(ray, 50.0f,LayerMask.GetMask("GameManage"));

        if (m_hitsEnemy.Length > 0)
        {
            if (screenColorRed == false)
            {
                sightChanger.ChangeRed();
                tarretScreenLeftSliderChanger.ChangeSliderFillRed();
                tarretScreenRightSliderChanger.ChangeSliderFillRed();
                screenColorRed = true;
            }

            rayHitFirstPosi = m_hitsEnemy[0].point;
        }
        else
        {
            if (screenColorRed == true)
            {
                sightChanger.ChangeBase();
                tarretScreenLeftSliderChanger.ChangeSliderFillBase();
                tarretScreenRightSliderChanger.ChangeSliderFillBase();
                screenColorRed = false;
            }

            rayHitFirstPosi = muzzle.transform.TransformPoint(rayDistance * 10);
        }
    }

    void KillEnemyFromRazer()
    {
        foreach (var hit in m_hitsEnemy)
        {
            if (hit.transform.gameObject.layer == 11)//layerがGameManageだったとき
            {
                hit.transform.GetComponent<GameStart>().StartGame();
                Instantiate(m_hitExplodeEffect, hit.point, Quaternion.identity);
                continue;
            }
            explosionForce(hit.point);
            Instantiate(m_hitExplodeEffect, hit.point, Quaternion.identity);
            EnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<EnemyDeath>();
            enemyDeath.OnDead();
            //hit.collider.enabled = false;
            //Destroy(hit.collider.gameObject);
        }
    }

    //レーザーのライン部分のスクリプト
    void FireEffectManager()
    {
        //razerLineRenderer = m_razerEffect.transform.GetChild(0).GetComponent<LineRenderer>();
        //m_razer = Instantiate(m_razerEffect, m_razerEffectInsPosi.transform.position, m_razerEffectInsPosi.transform.rotation);
        //LineRenderer razerLineRenderer = m_razer.transform.GetChild(2).gameObject.GetComponent<LineRenderer>();
        

        m_razerEffect.transform.position = m_razerEffectInsPosi.transform.position;
        m_razerEffect.transform.rotation = m_razerEffectInsPosi.transform.rotation;
        m_razerEffect.SetActive(true);
        razerLineRenderer.startWidth = 1.3f;
        razerLineRenderer.endWidth = 1.3f;
        DOTween.To(
            () => razerLineRenderer.startWidth,
            (x) => razerLineRenderer.startWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);
        DOTween.To(
            () => razerLineRenderer.endWidth,
            (x) => razerLineRenderer.endWidth = x,
            0,
            0.5f).SetEase(Ease.InQuad);
        
        //m_razerSteamEffect.SetActive(true);
        Invoke("FadeFire", razerExistTime);
        //Invoke("FadeFireLight", razerSteamExistTime);

        //FadeFire();
    }

    void FadeFire()
    {
        //razerLineRenderer.startWidth = 1.0f;
        //razerLineRenderer.endWidth = 1.0f;

        //Destroy(m_razer, razerExistTime);
        m_razerEffect.SetActive(false);
    }
    //void FadeFireLight()
    //{
    //    m_razerSteamEffect.SetActive(false);
    //}


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
        GameObject explode = Instantiate(explodeobj, hitPosi, Quaternion.identity);
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
            if (BaseTarretBrain.tarretCommandState != TarretCommand.Attack)
            {
                baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
            }
        }

    }
#endif
}