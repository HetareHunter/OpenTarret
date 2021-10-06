using System;
using System.Linq;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V���G�b�g�̃X�|�i�[�N���X(���Ԍo�߂ƃA�N�e�B�u�ȃV���G�b�g�I�u�W�F�N�g�̐��ŐV���ɃV���G�b�g���N�����������s��)
/// </summary>
public class SilhouetteActivateManager : MonoBehaviour, ISpawnable
{
    public List<GameObject> _registerSilhouettes = new List<GameObject>();
    Queue<SilhouetteActivatior> _silhouettes = new Queue<SilhouetteActivatior>();

    /// <summary> �A�N�e�B�u�ȃV���G�b�g�̐�</summary>
    int _activeSilhouetteNum = 0;
    [SerializeField] int _maxActiveSilhouetteNum = 3;

    /// <summary> �N���オ��܂ł̎��ԊԊu </summary>
    [SerializeField] float _spawnTime = 5.0f;
    float _nowTime = 0;

    /// <summary> �����܂ł̎��Ԃ��o�߂������ǂ��� </summary>
    bool _onSpawnTimePassed = false;
    /// <summary> �����ėǂ��󋵂����Ǘ�����t���O </summary>
    bool _spawnable = false;
    /// <summary> �X�|�i�[���N�����Ă��邩�ǂ��� </summary>
    public bool _onSpawn = false;

    public int ActiveSilhouetteNum
    {
        get
        {
            return _activeSilhouetteNum;
        }
        set
        {
            _activeSilhouetteNum += value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        JudgeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (_onSpawn)
        {
            if (_activeSilhouetteNum < _maxActiveSilhouetteNum)
            {
                CalculateSpawnTime();
            }
            JudgeSpawn();

            if (_spawnable)
            {
                ActivateSilhouette();
            }
        }
    }

    public void ActivateSilhouette()
    {
        _silhouettes.Dequeue().Activate();
        ChangeEnemyNum(1); //�G�̐���������
        _nowTime = 0;
        _onSpawnTimePassed = false;
        _spawnable = false;
    }

    public void ResetEnemies()
    {

    }
    public void ChangeEnemyNum(int num)
    {
        _activeSilhouetteNum += num;
    }
    public void SpawnStart()
    {
        _onSpawn = true;
        RegisterSilhouettes(_registerSilhouettes);
        JudgeSpawn();
    }
    public void SpawnEnd()
    {
        _onSpawn = false;
        ResetEnemies();
    }
    void JudgeSpawn()
    {
        if (_activeSilhouetteNum < _maxActiveSilhouetteNum && _onSpawnTimePassed)
        {
            _spawnable = true;
        }
        else
        {
            _spawnable = false;
        }
    }

    /// <summary>
    /// �O��G�������Ă���ǂꂾ�����Ԃ��o�����̂����v������
    /// </summary>
    void CalculateSpawnTime()
    {
        _nowTime += Time.deltaTime;
        if (_nowTime > _spawnTime)
        {
            _onSpawnTimePassed = true;
            _nowTime = 0;
        }
    }

    void RegisterSilhouettes(List<GameObject> registerSilhouettes)
    {
        registerSilhouettes = registerSilhouettes.OrderBy(x => Guid.NewGuid()).ToList();
        for (int i = 0; i < _registerSilhouettes.Count; i++)
        {
            _silhouettes.Enqueue(registerSilhouettes[i].GetComponentInChildren<SilhouetteActivatior>());
        }
    }
}
