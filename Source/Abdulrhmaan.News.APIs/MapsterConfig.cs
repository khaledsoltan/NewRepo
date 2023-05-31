using Abdulrhmaan.News.SQlServer;
using Abdulrhmaan.NewsSite.Data;
using Mapster;

namespace Abdulrhmaan.News.APIs
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Item, NewsDto>.NewConfig().TwoWays().PreserveReference(false);
            TypeAdapterConfig<User, RegisterUser>.NewConfig().TwoWays().PreserveReference(false);


        }
    }
}
