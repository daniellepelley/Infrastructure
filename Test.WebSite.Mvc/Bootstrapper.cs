using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Test.WebSite.Mvc.Development;
using Unity.Mvc4;
using Newton.Validation;


namespace Test.WebSite.Mvc
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IEntityRuleProviderFactory, RuleProviderFactory>();
            container.RegisterCollectionType<IMetaDataFilter, SplitWordsMetaDataFilter>();
            container.RegisterCollectionType<IMetaDataFilter, AddColonMetaDataFilter>();
            container.RegisterCollectionType<IMetaDataFilter, IsRequiredMetaDataFilter>();

            container.RegisterType<DataAnnotationsModelMetadataProvider, CustomMetaDataProvider>();   
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }

        public static IUnityContainer RegisterCollectionType<TFrom, TTo>(this IUnityContainer container)
            where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(typeof(TTo).FullName);
        }
    }
}