using PocketZone.SaveLoad;
using Zenject;

namespace PocketZone.Installer
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStorables();
        }

        private void BindStorables()
        {
            var player = Container.Resolve<Player.Player>();

            IStorable[] storables =
            {
                new PlayerSaverLoader(player)
            };

            Container
                .Bind<IStorable[]>()
                .FromInstance(storables)
                .AsSingle();
        }
    }
}