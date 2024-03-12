using AutoMapper;
using Domain.Models;
using Domain.Return.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            
            CreateMap<MovieReviewDTO, MovieReview>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.imdbID, opt => opt.MapFrom(src => src.imdbID))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => src.ReviewDate))
                
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore());

            
            CreateMap<MovieReview, MovieReviewDTO>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.imdbID, opt => opt.MapFrom(src => src.imdbID))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(src => src.ReviewDate));
        }
    }
}
