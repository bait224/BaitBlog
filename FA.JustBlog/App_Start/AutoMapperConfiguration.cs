using AutoMapper;

namespace FA.JustBlog.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void Config()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EntityModelMapper>();
                cfg.ValidateInlineMaps = false;
            });
        }
    }
}