//using UnityEngine;
//using Zenject;
//using Enemy;

//public class InvaderCommanderInstaller : MonoInstaller
//{
//    [SerializeField] GameObject invadeCommander;
//    public override void InstallBindings()
//    {
//        Container
//            .Bind<IInvaderMoveCommandable>() // Inject�A�g���r���[�g�����Ă���IChangeSightColor�^�̃t�B�[���h��
//            .To<InvaderMoveCommander>()
//            .FromComponentOn(invadeCommander) // TarretScreenSliderChanger�N���X�̃C���X�^���X�𒍓�����
//            .AsTransient();
//    }
//}