using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MoviesRememberServices.Interface;
using MoviesRememberServices.MailService;
using StructureMap;
using PetaPoco;
using MoviesRememberDomain;
using AutoMapper;
using MoviesRememberDao.Interface;
using MoviesRememberDao;
using MoviesRememberDB;
using ServiceStack.Redis;

namespace MoviesRememberServices.Utils
{
    public static class Bootstrapper
    {
        private static void RegisterDependencies()
        {
            string host = ConfigurationManager.AppSettings["REDISTOGO_URL"];
            int port = int.Parse(ConfigurationManager.AppSettings["REDISTOGO_PORT"]);
            string pwd = ConfigurationManager.AppSettings["REDISTOGO_PWD"];

            new LogEvent("Password: " + pwd).Raise();


            ObjectFactory.Initialize(
                x => x.Scan(
                    scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                        scan.LookForRegistries();
                    }));

            ObjectFactory.Container.Configure(
                c => c.For<Database>().Use<MoviesRememberDBDB>()
                );

            if (ConfigurationManager.AppSettings["Environment"] == "Release")
            {
                var url = new Uri(host);
                ObjectFactory.Container.Configure(
                    c => c.For<IRedisClient>().Use(new RedisClient(url))
                    );
            }
            else
            {
                ObjectFactory.Container.Configure(
                    c => c.For<IRedisClient>().Use(new RedisClient(host, port, pwd))
                    );
            }

            ObjectFactory.Container.Configure(
                c => c.For<IUserActionsDAO>().Use<UserActionsDAO>()
                );

            //ObjectFactory.Container.Configure(
            //    c => c.For<IMoviesShowingService>().Use<MoviesShowingService>()
            //    );

            //ObjectFactory.Container.Configure(
            //    c => c.For<IUserService>().Use<UserService>()
            //    );

            //ObjectFactory.Container.Configure(
            //   c => c.For<ISearchService>().Use<SearchService>()
            //   );

            ObjectFactory.Container.Configure(
               c => c.For<AbstractUserMovieDAO>().Use<UserMovieDAO>()
               );

            ObjectFactory.Container.Configure(
                c => c.For<IMailService>().Use(new MailServiceClient("BasicHttpBinding_IMailService"))
                );
#if DEBUG
            // Place a breakpoint on the line and see the configuration of StructureMap.
            string configuration = ObjectFactory.WhatDoIHave();
#endif
        }

        private static void InitializeMapper()
        {
            Mapper.CreateMap<TinyMovie, user_movie>()
                .ForMember(dest => dest.user_movie_picture, opt => opt.MapFrom(src => src.PictureUrl))
                .ForMember(dest => dest.user_movie_release_date, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.user_movie_api_id, opt => opt.MapFrom(src => src.ApiId))
                .ForMember(dest => dest.user_movie_title, opt => opt.MapFrom(src => src.Title));

            Mapper.CreateMap<Movie, TinyMovie>();

            Mapper.CreateMap<UserMovie, user_movie>()
                .ForMember(dest => dest.user_movie_picture, opt => opt.MapFrom(src => src.PictureUrl))
                .ForMember(dest => dest.user_movie_release_date, opt => opt.MapFrom(src => DateTime.Parse(src.ReleaseDate)))
                .ForMember(dest => dest.user_movie_id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.user_movie_rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.user_movie_user_id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.user_movie_api_id, opt => opt.MapFrom(src => src.ApiId))
                .ForMember(dest => dest.user_movie_title, opt => opt.MapFrom(src => src.Title));

            Mapper.CreateMap<user_movie, UserMovie>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.user_movie_picture))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.user_movie_release_date.ToShortDateString()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.user_movie_id))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.user_movie_rate))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_movie_user_id))
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.user_movie_api_id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.user_movie_title));

            Mapper.CreateMap<user_movie, TinyMovie>()
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.user_movie_picture))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.user_movie_release_date.ToShortDateString()))
                .ForMember(dest => dest.ApiId, opt => opt.MapFrom(src => src.user_movie_api_id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.user_movie_title));
        }

        public static void Bootstrap()
        {
            RegisterDependencies();
            InitializeMapper();
        }

        public static T GetInstance<T>()
        {
            Bootstrap();
            return ObjectFactory.GetInstance<T>();
        }


    }
}
