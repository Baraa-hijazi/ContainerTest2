
using MQTTnet;
using MQTTnet.Server;

namespace ContainerTest2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithEncryptedEndpoint()
                .WithTlsEndpointReuseAddress()
                .WithDefaultEndpointPort(1883)
                .WithEncryptedEndpointPort(8883);

            var mqttServer = new MqttFactory().CreateMqttServer(optionsBuilder.Build());
            mqttServer.StartAsync();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}