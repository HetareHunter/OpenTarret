using UnityEngine;
using Zenject;
using Enemy;

public class InvaderCommanderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<IInvaderMoveCommandable>() // InjectアトリビュートがついているIChangeSightColor型のフィールドに
            .To<InvaderMoveCommander>()
            .FromNew() // TarretScreenSliderChangerクラスのインスタンスを注入する
            .AsTransient();
    }
}