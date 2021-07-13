using UnityEngine;
using Zenject;
using Tarret;

public class TarretStateInstaller : MonoInstaller
{
    [SerializeField] GameObject tarret;
    public override void InstallBindings()
    {
        Container
            .Bind<ITarretState>() // InjectアトリビュートがついているIChangeSightColor型のフィールドに
            .To<TarretStateManager>()
            .FromComponentOn(tarret) // TarretScreenSliderChangerクラスのインスタンスを注入する
            .AsTransient();
    }
}