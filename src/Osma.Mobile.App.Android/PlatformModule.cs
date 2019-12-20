using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hyperledger.Aries.Agents;
using Microsoft.Extensions.DependencyInjection;
using Osma.Mobile.App.Services;

namespace Osma.Mobile.App.Droid
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule(new CoreModule());
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAriesFramework(frameworkBuilder =>
            {
                frameworkBuilder.RegisterAgent(options =>
                {

                    //options.WalletConfiguration = new Hyperledger.Aries.Storage.WalletConfiguration { StorageConfiguration = new Hyperledger.Aries.Storage.WalletConfiguration.WalletStorageConfiguration {
                    //    Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".indy_client")
                        
                    //}, Id = Guid.NewGuid().ToString() };
                    options.GenesisFilename = "pool_genesis.txn";
                    options.WalletCredentials = new Hyperledger.Aries.Storage.WalletCredentials { Key = "LocalWalletKey" };
                    // options.AgentNam
                });

                frameworkBuilder.AddAgentProvider();
            });


           
            builder.Populate(serviceCollection);

            

            //builder.RegisterModule(new ServicesModule());
        }
    }
}