using AutoMapper;
using BookStore.RedisMessageQueue.Models;
using BookStore.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Api.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<BookItem, PurchaseBookRedisModel>()
                .ForMember(dest=> dest.Title, opt=> opt.MapFrom(s=> s.VolumeInfo.Title))
                .ForMember(dest=> dest.Authors, opt=> opt.MapFrom(s=> s.VolumeInfo.Authors))
                .ForMember(dest=> dest.Subtitle, opt=> opt.MapFrom(s=> s.VolumeInfo.Subtitle))
                .ForMember(dest=> dest.PublishedDate, opt=> opt.MapFrom(s=> s.VolumeInfo.PublishedDate))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(s=> DateTime.UtcNow))
                ;
        }
    }
}
