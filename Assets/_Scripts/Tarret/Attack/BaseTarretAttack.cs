using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Managers;
using UniRx;
using DG.Tweening;

public class BaseTarretAttack : MonoBehaviour
{
    [SerializeField] TarretAttackData tarretAttackData;
    AttackIntervalCounter attackInterval;

    [SerializeField] GameObject muzzleFrameJoint;

    [SerializeField] Vector3 rayDistance;
    [SerializeField] GameObject rayOfOrigin;

    [SerializeField] GameObject m_razerEffect;
    [SerializeField] GameObject[] m_wasteHeatEffects;
    int wasteHeatIndex = 0;
    [SerializeField] GameObject m_shockWaveEffect;
    [SerializeField] GameObject[] m_hitExplodeEffects;
    int hitExplodeIndex = 0;
    [SerializeField] GameObject[] explodeForces;
    int explodeForceIndex = 0;

    [SerializeField] GameObject m_razerEffectInsPosi;
    [SerializeField] GameObject m_wasteHeatEffectInsPosi;
    [SerializeField] GameObject m_shockWaveEffectInsPosi;

    /// <summary> 攻撃可能かどうか判定する </summary>
    public bool attackable = true;

    LineRenderer razerLineRenderer;

    [SerializeField] GameObject muzzle;
    float muzzleRadius;
    AudioPlayer muzzleAudio;

    BaseTarretBrain baseTarretBrain;

    [SerializeField] GameObject magazine;
    MagazineRotate magazineRotate;
    [SerializeField] float untilRotateMagazine = 0.3f;

    

    /// <summary> スクリーンに投影する照準についての変数 </summary>
    [SerializeField] GameObject sight;
    SightChanger sightChanger;
    [SerializeField] GameObject sightLeftSlider;
    [SerializeField] GameObject sightRightSlider;
    TarretScreenSliderChanger tarretScreenLeftSliderChanger;
    TarretScreenSliderChanger tarretScreenRightSliderChanger;
    bool screenColorRed = false;

    

    //　当たったコライダを入れておく変数
    RaycastHit[] m_hitsEnemy;

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

    /// <summary>
    /// レイを飛ばして当たったオブジェクトが何なのかを判定する関数
    /// </summary>
    void RaySearchObject()
    {
        //　飛ばす位置と飛ばす方向を設定
        Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);

        //　Sphereの形でレイを飛ばしEnemy、GameManagerレイヤーのオブジェクトをm_hitsEnemyに入れる
        m_hitsEnemy = Physics.SphereCastAll(ray, muzzleRadius, rayDistance.z, LayerMask.GetMask("Enemy", "GameManage"));

        if (m_hitsEnemy.Length > 0)
        {
            //スクリーンのUI表示の色替え
            if (screenColorRed == false)
            {
                sightChanger.ChangeRed();
                tarretScreenLeftSliderChanger.ChangeSliderFillRed();
                tarretScreenRightSliderChanger.ChangeSliderFillRed();
                screenColorRed = true;
            }
        }
        else
        {
            //スクリーンのUI表示の色替え
            if (screenColorRed == true)
            {
                sightChanger.ChangeBase();
                tarretScreenLeftSliderChanger.ChangeSliderFillBase();
                tarretScreenRightSliderChanger.ChangeSliderFillBase();
                screenColorRed = false;
            }
        }
    }

    /// <summary>
    /// 攻撃したときの具体的な処理、現在タグで区別している
    /// </summary>
    void KillEnemyFromRazer()
    {
        foreach (var hit in m_hitsEnemy)
        {
            if (hit.transform.gameObject.tag == "GameStart")//タグがGameStartだったとき
            {
                hit.transform.GetComponent<GameStart>().StartGame();
                //爆発エフェクトの再生
                m_hitExplodeEffects[hitExplodeIndex].transform.position = hit.point;
                m_hitExplodeEffects[hitExplodeIndex].SetActive(true);
                hitExplodeIndex++;
            }
            else
            {
                ExplosionForce(hit.point);
                //爆発エフェクトの再生
                m_hitExplodeEffects[hitExplodeIndex].transform.position = hit.point;
                m_hitExplodeEffects[hitExplodeIndex].SetActive(true);
                hitExplodeIndex++;
                EnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<EnemyDeath>();
                enemyDeath.OnDead();
            }

            if (hitExplodeIndex >= m_hitExplodeEffects.Length)
            {
                hitExplodeIndex = 0;
            }
        }
    }

    /// <summary>
    /// レーザーのライン部分のスクリプト、存在しているものを移動して、、徐々に消えていくようにしている
    /// </summary>
    void FireEffectManager()
    {
        m_razerEffect.transform.position = m_razerEffectInsPosi.transform.position;
        m_razerEffect.transform.rotation = m_razerEffectInsPosi.transform.rotation;
        m_razerEffect.SetActive(true);

        razerLineRenderer.startWidth = tarretAttackData.razerWidth;
        razerLineRenderer.endWidth = tarretAttackData.razerWidth;
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

        Invoke("FadeFire", tarretAttackData.razerExistTime);
    }

    void FadeFire()
    {
        m_razerEffect.SetActive(false);
    }

    //ここまでレーザーのライン部分のスクリプト

    /// <summary>
    /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
    /// </summary>
    void WasteHeatEffectManager()
    {
        m_wasteHeatEffects[wasteHeatIndex].SetActive(true);
        wasteHeatIndex++;
        if (wasteHeatIndex >= m_wasteHeatEffects.Length)
        {
            wasteHeatIndex = 0;
        }
    }

    /// <summary>
    /// 衝撃波を徐々に大きくしていき、一定時間で消滅する
    /// </summary>
    void ShockWaveManager()
    {
        //m_shockWave = Instantiate(m_shockWaveEffect, m_shockWaveEffectInsPosi.transform.position,
        //    m_shockWaveEffectInsPosi.transform.rotation);
        m_shockWaveEffect.transform.position = m_shockWaveEffectInsPosi.transform.position;
        m_shockWaveEffect.transform.rotation = m_shockWaveEffectInsPosi.transform.rotation;
        m_shockWaveEffect.SetActive(true);

        //Destroy(m_shockWave, tarretAttackData.shockWaveExistTime);
    }

    /// <summary>
    /// 攻撃が当たった時の吹き飛ばす力。オブジェクトを物理的に高速でぶつけている
    /// </summary>
    /// <param name="hitPosi"></param>
    void ExplosionForce(Vector3 hitPosi)
    {
        //GameObject explode = Instantiate(explodeForces, hitPosi, Quaternion.identity);
        //BeamPower beamPower = explode.GetComponent<BeamPower>();
        explodeForces[explodeForceIndex].transform.position = hitPosi;
        explodeForces[explodeForceIndex].transform.rotation = muzzle.transform.rotation;
        explodeForces[explodeForceIndex].SetActive(true);

        explodeForceIndex++;
        if (explodeForceIndex >= explodeForces.Length)
        {
            explodeForceIndex = 0;
        }
        //beamPower.Movement(muzzleFrameJoint.transform.forward);
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