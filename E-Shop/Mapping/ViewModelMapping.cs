using AutoMapper;
using E_Shop.Models;
using EntityLayer.Entities;

namespace E_Shop.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product,ProductList2ViewModel>().ReverseMap();
            CreateMap<Product, ProductList3ViewModel>().ReverseMap();
            CreateMap<Product,ProductUpdateViewModel>().ReverseMap();
            //CreateMap<Comment,ProductCommentViewModel>().ReverseMap();
            //CreateMap<User, ProductCommentViewModel>().ReverseMap();
        }
    }
}
