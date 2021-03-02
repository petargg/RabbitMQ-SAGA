using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using System;

namespace rabbitmq
{
    public class RabbitMqBus
    {
        public static IBusControl ConfigureBus(IServiceProvider provider, Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
         registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseHealthCheck(provider);
                var host = cfg.Host(BusConstants.RabbitMqUri, hst =>
                {
                    hst.Username(BusConstants.UserName);
                    hst.Password(BusConstants.Password);
                });

                cfg.ConfigureEndpoints(provider, KebabCaseEndpointNameFormatter.Instance);

                registrationAction?.Invoke(cfg, host);
            });
        }
    }
}
