using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGuid : MonoBehaviour
{
    [SerializeField] GameObject _handMesh_L;
    [SerializeField] GameObject _handMesh_R;
    [SerializeField] GameObject _handController_L;
    [SerializeField] GameObject _handController_R;

    SkinnedMeshRenderer _handMeshRenderer_L;
    SkinnedMeshRenderer _handMeshRenderer_R;
    MeshRenderer _controllerMeshRenderer_L;
    MeshRenderer _controllerMeshRenderer_R;

    private void Awake()
    {
        _handMeshRenderer_L = _handMesh_L.GetComponent<SkinnedMeshRenderer>();
        _handMeshRenderer_R = _handMesh_R.GetComponent<SkinnedMeshRenderer>();
        _controllerMeshRenderer_L = _handController_L.GetComponent<MeshRenderer>();
        _controllerMeshRenderer_R = _handController_R.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controllerMeshRenderer_L.enabled = false;
        _controllerMeshRenderer_R.enabled = false;
    }

    public void SwicthHandMesh(bool useController, Hand hand)
    {
        if (useController) //手がコントローラになる
        {
            if (hand == Hand.Left)
            {
                _handMeshRenderer_L.enabled = false;
                _controllerMeshRenderer_L.enabled = true;
            }
            else if (hand == Hand.Right)
            {
                _handMeshRenderer_R.enabled = false;
                _controllerMeshRenderer_R.enabled = true;
            }
        }
        else //コントローラが手になる
        {
            if (hand == Hand.Left)
            {
                _handMeshRenderer_L.enabled = true;
                _controllerMeshRenderer_L.enabled = false;
            }
            else if (hand == Hand.Right)
            {
                _handMeshRenderer_R.enabled = true;
                _controllerMeshRenderer_R.enabled = false;
            }
        }
    }
}
