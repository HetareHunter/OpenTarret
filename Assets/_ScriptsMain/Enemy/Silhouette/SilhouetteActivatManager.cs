using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteActivatManager : MonoBehaviour, ISpawnable
{
    public List<GameObject> _silhouettes = new List<GameObject>();

    /// <summary> �A�N�e�B�u�ȃV���G�b�g�̐�</summary>
    public int _activeSilhouetteNum = 0;
    [SerializeField] int _maxActiveSilhouetteNum = 3;

    /// <summary> �N���オ��܂ł̎��ԊԊu </summary>
    [SerializeField] float _spawnTime = 3.0f;
    float _nowTime = 0;

    /// <summary> �����܂ł̎��Ԃ��o�߂������ǂ��� </summary>
    bool _onSpawnTimePassed = false;
    /// <summary> �����ėǂ��󋵂����Ǘ�����t���O </summary>
    bool _spawnable = false;
    /// <summary> �X�|�i�[���N�����Ă��邩�ǂ��� </summary>
    public bool _onSpawn = false;

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
                EnemySpawn();
            }
        }
    }

    public void EnemySpawn()
    {
        int index = Random.Range(0, _silhouettes.Count); //�ǂ��ɓG�𐶐����邩�̗���
        //enemies.Add(Instantiate(enemy, spawners[index].transform.position, Quaternion.identity)); //�G�𐶐�����
        //_silhouettes[index].
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

    }
    public void SpawnStart()
    {

    }
    public void SpawnEnd()
    {

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
}
