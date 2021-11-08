using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class ExplosionPool : ObjectPool<ExplosionForce>
{
    private readonly ExplosionForce _original;

    /// <summary>
    /// オリジナルを渡して初期化
    /// </summary>
    public ExplosionPool(ExplosionForce original)
    {
        //オリジナルは非表示に
        _original = original;
        //_original.gameObject.SetActive(false);
    }

    //インスタンスを作る処理
    protected override ExplosionForce CreateInstance()
    {
        //オリジナルを複製してインスタンス作成(オリジナルと同じ親の下に配置)
        return ExplosionForce.Instantiate(_original, _original.transform.parent);
    }

    //プールからオブジェクトを取得する前に実行される
    protected override void OnBeforeRent(ExplosionForce instance)
    {
        Debug.Log($"{instance.name}がプールから取り出されました");
        base.OnBeforeRent(instance);
    }

    //オブジェクトがプールに戻る前に実行される
    protected override void OnBeforeReturn(ExplosionForce instance)
    {
        Debug.Log($"{instance.name}がプールに戻されました");
        base.OnBeforeReturn(instance);
    }
}
