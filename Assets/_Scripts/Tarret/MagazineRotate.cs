using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagazineRotate : MonoBehaviour
{
    Quaternion nowAngle;
    [SerializeField] GameObject baseTarret;
    TarretAttack tarretAttack;
    // Start is called before the first frame update
    void Start()
    {
        nowAngle = transform.rotation;
        tarretAttack = baseTarret.GetComponent<TarretAttack>();
    }


    public void RotateMagazine()
    {
        transform.DORotate(Vector3.forward * 60f, 1f, mode: RotateMode.LocalAxisAdd)
            .OnComplete(() => tarretAttack.EndAttack());

    }
}
