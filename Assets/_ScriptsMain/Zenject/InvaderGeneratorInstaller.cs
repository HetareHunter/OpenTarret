using Zenject;
using UnityEngine;

public class InvaderGeneratorInstaller : MonoInstaller
{
    [SerializeField] GameObject spawner;
    public override void InstallBindings()
    {
        Container
            .Bind<ISpawnable>() // InjectアトリビュートがついているIChangeSightColor型のフィールドに
            .To<InvaderGenerator>()
            .FromComponentOn(spawner) // TarretScreenSliderChangerクラスのインスタンスを注入する
            .AsTransient();
    }
}
