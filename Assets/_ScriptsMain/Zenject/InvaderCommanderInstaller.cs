using UnityEngine;
using Zenject;
using Enemy;

public class InvaderCommanderInstaller : MonoInstaller
{
    [SerializeField] GameObject invadeCommander;
    public override void InstallBindings()
    {
        Container
            .Bind<IInvaderMoveCommandable>() // InjectアトリビュートがついているIChangeSightColor型のフィールドに
            .To<InvaderMoveCommander>()
            .FromComponentOn(invadeCommander) // TarretScreenSliderChangerクラスのインスタンスを注入する
            .AsTransient();
    }
}