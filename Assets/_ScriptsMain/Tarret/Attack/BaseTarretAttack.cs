﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using Zenject;

namespace Tarret
{
    /// <summary>
    /// タレットの攻撃関係の処理をまとめているクラス
    /// 機能が集中しすぎてきたため機能を分散すること
    /// </summary>
    public class BaseTarretAttack : MonoBehaviour
    {
        [SerializeField] TarretAttackData tarretAttackData;

        AttackIntervalCounter attackInterval;
        TarretStateManager baseTarretBrain;
        LineRenderer razerLineRenderer;
        AudioPlayer muzzleAudio;
        MagazineRotate magazineRotate;
        SightChanger sightChanger;
        [Inject]
        private IChangeSightColor changeSightCoror;


        /// <summary>レイキャストの長さ </summary>
        [SerializeField] Vector3 rayDistance;
        /// <summary>レイの原点。ここから伸びていく </summary>
        [SerializeField] GameObject rayOfOrigin;

        //------------------ここから撃った時のエフェクト関連の変数-------------------
        /// <summary>ビーム本体のエフェクト </summary>
        [SerializeField] GameObject m_razerEffect;
        /// <summary>廃熱のエフェクト </summary>
        [SerializeField] GameObject[] m_wasteHeatEffects;
        int wasteHeatIndex = 0;
        /// <summary>衝撃波のエフェクト </summary>
        [SerializeField] GameObject m_shockWaveEffect;
        /// <summary>爆発のエフェクト </summary>
        [SerializeField] GameObject[] m_hitExplodeEffects;

        int hitExplodeIndex = 0;
        /// <summary>衝撃の物理的な力 </summary>
        [SerializeField] GameObject[] explodeForces;
        int explodeForceIndex = 0;

        /// <summary>ビームの生成位置 </summary>
        [SerializeField] GameObject m_razerEffectInsPosi;
        /// <summary>廃熱エフェクトの生成位置 </summary>
        [SerializeField] GameObject m_wasteHeatEffectInsPosi;
        /// <summary>衝撃波の生成位置 </summary>
        [SerializeField] GameObject m_shockWaveEffectInsPosi;

        //-----------------エフェクト関連ここまで---------------------

        /// <summary> 攻撃可能かどうか判定する </summary>
        [NonSerialized] public bool attackable = true;

        [SerializeField] GameObject muzzle;
        float muzzleRadius;
        [SerializeField] GameObject magazine;

        /// <summary> スクリーンに投影する照準についての変数 </summary>
        [SerializeField] GameObject sight;
        bool screenColorRed = false;

        /// <summary> 当たったオブジェクトの情報を入れておく変数 </summary>
        RaycastHit[] m_hitsEnemy;

        private void Start()
        {
            baseTarretBrain = GetComponent<TarretStateManager>();
            //　弾の半径を取得
            muzzleRadius = muzzle.GetComponent<SphereCollider>().radius;

            muzzleAudio = muzzle.GetComponent<AudioPlayer>();
            magazineRotate = magazine.GetComponent<MagazineRotate>();
            sightChanger = sight.GetComponent<SightChanger>();
            attackInterval = GetComponent<AttackIntervalCounter>();
            razerLineRenderer = m_razerEffect.transform.GetChild(0).GetComponent<LineRenderer>();
        }

        void FixedUpdate()
        {
            RaySearchObject();
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
                    changeSightCoror.ChangeSliderFillRed();
                    screenColorRed = true;
                }
            }
            else
            {
                //スクリーンのUI表示の色替え
                if (screenColorRed == true)
                {
                    sightChanger.ChangeBase();
                    changeSightCoror.ChangeSliderFillBase();
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
                //爆発したときの力となるオブジェクトの生成
                ExplosionForce(hit.point);
                //爆発エフェクトの再生
                m_hitExplodeEffects[hitExplodeIndex].transform.position = hit.point;
                m_hitExplodeEffects[hitExplodeIndex].SetActive(true);
                hitExplodeIndex++;
                IEnemyDeath enemyDeath = hit.collider.gameObject.GetComponent<IEnemyDeath>();
                enemyDeath.OnDead();

                if (hitExplodeIndex >= m_hitExplodeEffects.Length)
                {
                    hitExplodeIndex = 0;
                }
            }
        }

        /// <summary>
        /// レーザーのライン部分のスクリプト、存在しているものを移動して、、徐々に消えていくようにしている
        /// </summary>
        void InstanceFireEffect()
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

        }

        void FadeFire()
        {
            m_razerEffect.SetActive(false);
        }

        /// <summary>
        /// 廃熱エフェクトの実体化から消滅までの管理をするメソッド
        /// </summary>
        void InstanceWasteHeatEffect()
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
            m_shockWaveEffect.transform.position = m_shockWaveEffectInsPosi.transform.position;
            m_shockWaveEffect.transform.rotation = m_shockWaveEffectInsPosi.transform.rotation;
            m_shockWaveEffect.SetActive(true);
        }

        /// <summary>
        /// 攻撃が当たった時の吹き飛ばす力。オブジェクトを物理的に高速でぶつけている
        /// </summary>
        /// <param name="hitPosi"></param>
        void ExplosionForce(Vector3 hitPosi)
        {
            explodeForces[explodeForceIndex].transform.position = hitPosi;
            explodeForces[explodeForceIndex].transform.rotation = muzzle.transform.rotation;
            explodeForces[explodeForceIndex].SetActive(true);

            explodeForceIndex++;
            if (explodeForceIndex >= explodeForces.Length)
            {
                explodeForceIndex = 0;
            }
        }

        void EffectFadeTask()
        {
            FadeFire();
            magazineRotate.RotateMagazine();
        }


        /// <summary>
        /// 攻撃したとき、TarretCommandステートがAttackになったときに一度だけ呼ばれるメソッド。
        /// </summary>
        public void BeginAttack()
        {
            muzzleAudio.AudioPlay();
            InstanceFireEffect();
            InstanceWasteHeatEffect();
            ShockWaveManager();
            KillEnemyFromRazer();

            attackable = false;
            attackInterval.countStart = true;
        }


        public void EndAttack()
        {
            EffectFadeTask();
            attackable = true;
        }


#if UNITY_EDITOR

        private void Update()
        {

            if (Input.GetKeyDown("space"))
            {
                if (TarretStateManager.tarretCommandState != TarretCommand.Attack)
                {
                    baseTarretBrain.ChangeTarretState(TarretCommand.Attack);
                }
            }

        }
#endif
    }
}