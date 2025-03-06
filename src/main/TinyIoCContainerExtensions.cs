using ei8.Cortex.Coding;
using ei8.Cortex.Coding.Persistence;
using ei8.Cortex.Library.Client.Out;
using Microsoft.Extensions.Options;
using Nancy.TinyIoc;
using System.Collections.Generic;
using System.Net.Http;

namespace ei8.Extensions.DependencyInjection.Coding.Persistence
{
    public static class TinyIoCContainerExtensions
    {
        public static void AddNetworkRepository(this TinyIoCContainer container, string cortexLibraryOutBaseUrl, int queryResultLimit, string appUserId)
        {
            container.Register<INetworkRepository>(
                (tic, npo) => new NetworkRepository(
                    container.Resolve<INeuronQueryClient>(),
                    cortexLibraryOutBaseUrl,
                    queryResultLimit,
                    appUserId
                )
            );
        }

        public static void AddExternalReferenceRepository(this TinyIoCContainer container, string eventSourcingInBaseUrl, string eventSourcingOutBaseUrl, string cortexLibraryOutBaseUrl, string identityAccessOutBaseUrl, string appUserId)
        {
            container.Register<IExternalReferenceRepository>(
                (tic, npo) => new ExternalReferenceRepository(
                    container.Resolve<IHttpClientFactory>(),
                    container.Resolve<INeuronQueryClient>(),
                    eventSourcingInBaseUrl,
                    eventSourcingOutBaseUrl,
                    cortexLibraryOutBaseUrl,
                    identityAccessOutBaseUrl,
                    container.Resolve<IOptions<List<ExternalReference>>>(),
                    appUserId
                )
            );
        }
    }
}
