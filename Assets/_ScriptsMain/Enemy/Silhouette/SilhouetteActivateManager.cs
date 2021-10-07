using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

/// <summary>
/// �V���G�b�g�̃X�|�i�[�N���X(���Ԍo�߂ƃA�N�e�B�u�ȃV���G�b�g�I�u�W�F�N�g�̐��̏����ŐV���ɃV���G�b�g���N�����������s��)
/// </summary>
public class SilhouetteActivateManager : MonoBehaviour, ISpawnable
{
    IGameStateChangable _gameStateChangable;
    [SerializeField] GameObject _gameManager;

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
    /// <summary> �Q�[�����I������܂łɋN������V���G�b�g�̐� </summary>
    int _maxActivateSilhouetteNum;
    int _activatedSilhouetteNum = 0;

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
    private void Awake()
    {
        _gameStateChangable = _gameManager.GetComponent<IGameStateChangable>();
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
        _activatedSilhouetteNum++;
        ChangeEnemyNum(1); //�G�̐���������

        _silhouettes.Enqueue(_silhouettes.Peek());
        _silhouettes.Dequeue().Activate();

        _nowTime = 0;
        _onSpawnTimePassed = false;
        _spawnable = false;
    }

    public void ResetEnemies()
    {
        foreach (var item in _silhouettes)
        {
            item.Reset();
        }
    }
    public void ChangeEnemyNum(int num)
    {
        _activeSilhouetteNum += num;
        if (_activatedSilhouetteNum >= _maxActivateSilhouetteNum)
        {
            _onSpawn = false;
        }

        if (num < 0)
        {
            JudgeGameFinish();
        }
    }
    public void SpawnStart()
    {
        _onSpawn = true;
        RegisterSilhouettes(_registerSilhouettes);
        JudgeSpawn();
    }
    public void SpawnEnd()
    {
        _activatedSilhouetteNum = 0;
        _activeSilhouetteNum = 0;
        _nowTime = 0;
        _onSpawnTimePassed = false;
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

    /// <summary>
    /// �I���ăQ�[���Ɏg���V���G�b�g��o�^����
    /// �����œo�^�����V���G�b�g�����Ԍo�߂ŋN�����A�������ł���悤�ɂȂ�
    /// </summary>
    /// <param name="registerSilhouettes">�Ώۂ̃V���G�b�g�̓C���X�y�N�^�[����ݒ肷��</param>
    void RegisterSilhouettes(List<GameObject> registerSilhouettes)
    {
        _maxActivateSilhouetteNum = registerSilhouettes.Count;
        registerSilhouettes = registerSilhouettes.OrderBy(x => Guid.NewGuid()).ToList();
        for (int i = 0; i < _registerSilhouettes.Count; i++)
        {
            _silhouettes.Enqueue(registerSilhouettes[i].GetComponentInChildren<SilhouetteActivatior>());
        }
    }

    void JudgeGameFinish()
    {
        if (!_onSpawn)
        {
            _gameStateChangable.ChangeGameState(GameState.End);
        }
    }
}
