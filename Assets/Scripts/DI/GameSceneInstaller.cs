using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject vfxObj;
    [SerializeField]
    private GameObject SpawnerObj;
    [SerializeField]
    private GameObject SliderObj;
    public override void InstallBindings()
    {
        Container.Bind<FX>().FromComponentOn(vfxObj).AsSingle();
        Container.Bind<CubeSpawner>().FromComponentOn(SpawnerObj).AsSingle();
        Container.Bind<TouchSlider>().FromComponentOn(SliderObj).AsSingle();
    }
}