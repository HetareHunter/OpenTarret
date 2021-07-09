using UnityEngine;
using Zenject;

public class SpwanerInstaller : MonoInstaller
{
    [SerializeField] GameObject spawner;
    public override void InstallBindings()
    {
        Container
            .Bind<ISpawner>() // InjectアトリビュートがついているIChangeSightColor型のフィールドに
            .To<SpawnerManager>()
            .FromComponentOn(spawner) // TarretScreenSliderChangerクラスのインスタンスを注入する
            .AsTransient();
    }
}