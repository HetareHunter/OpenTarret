using Zenject;
using UnityEngine;
using Enemy;

public class InvaderGeneratorInstaller : MonoInstaller
{
    [SerializeField] GameObject spawner;
    public override void InstallBindings()
    {
        Container
            .Bind<ISpawnable>() // Inject�A�g���r���[�g�����Ă���IChangeSightColor�^�̃t�B�[���h��
            .To<InvaderGenerator>()
            .FromComponentOn(spawner) // TarretScreenSliderChanger�N���X�̃C���X�^���X�𒍓�����
            .AsTransient();
    }
}
