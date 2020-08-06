using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using MyBooks.Core.Profiles;
using MyBooks.Data.Entities;
using MyBooks.Data.Models;
using System.Linq;

namespace MyBooks.Core.Profiles
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                //cfg.ForAllMaps((typeMap, mappingExpression) => mappingExpression.MaxDepth(1));
                cfg.AddProfile<MappingProfile>();
               
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Books, BooksEntity>()
                .ForMember(dto => dto.Catalogs, opt => opt.MapFrom(x => x.Catalogs_Books.Select(y => y.Catalog)))
                .MaxDepth(1)
                //.ForMember(dto => dto.CatalogIds, opt => opt.MapFrom(x => x.Catalogs_Books.Select(y => y.Catalog.CatalogId)))
                //.ForMember(dto=>dto.Catalogs, opt=>opt.Ignore())
                .ReverseMap()
                .ForPath(x => x.BookId, x => x.Ignore());


            CreateMap<Users, UserEntity>()
                 //.ForMember(dto => dto.AddressId, opt => opt.MapFrom(x => x.AddressId))
                .ForPath(x => x.UserRole, x => x.Ignore())
                .MaxDepth(1)
                .ReverseMap()
                .ForPath(x => x.UserId, x => x.Ignore())
                .ForPath(x => x.AddressId, x => x.Ignore())
                .ForPath(x => x.Address, x => x.Ignore())
                .ForPath(x => x.UserRole, x => x.Ignore());


            CreateMap<UserRoles, UserRolesEntity>()
                //.ForPath(x => x.Users, x => x.Ignore())
                .MaxDepth(1)
                .ReverseMap()
                .ForPath(x => x.UserRoleId, x => x.Ignore())
                .ForPath(x => x.Users, x => x.Ignore());


            CreateMap<Shopping_Carts, Shopping_Cart_Entity>()
                .ForPath(x => x.User.Books, x => x.Ignore())
               .MaxDepth(1)
               .ReverseMap()
               .ForPath(x => x.Cart_Items, x => x.Ignore())
               .ForPath(x => x.User, x => x.Ignore())
               .ForPath(x => x.CartId, x => x.Ignore())
              /* .ForPath(x => x.UserId, x => x.Ignore())*/;

            CreateMap<Cart_items, Cart_Items_Entity>()
               //.ForPath(x => x.Users, x => x.Ignore())
               .ForPath(dto=>dto.Book, opt => opt.MapFrom(x => x.Book))
               .MaxDepth(1)
               .ReverseMap()
               .ForPath(x => x.Shop_Carts, x => x.Ignore())
               .ForPath(x => x.Book, x => x.Ignore())
               //.ForPath(x => x.BookId, x => x.Ignore())
               .ForPath(x => x.CartId, x => x.Ignore())
               .ForPath(x => x.ItemId, x => x.Ignore());

            CreateMap<Addresses, AddressEntity>()
                 /*.ForMember(dto => dto.Supplier, x => x.Ignore()*//*opt => opt.MapFrom(x=>x.Supplier))*/
                .MaxDepth(1)
                .ReverseMap()
                .ForPath(x => x.AddressId, x => x.Ignore())
                .ForPath(x => x.UsesId, x => x.Ignore())
                .ForMember(dto => dto.User, x => x.Ignore());

            CreateMap<Catalogs, CatalogsEntity>()
                .ForMember(dto => dto.Books, opt => opt.MapFrom(x => x.Catalogs_Books.Select(y => y.Book)))
                .MaxDepth(1)
                //.ForMember(dto => dto.Books.Select(e=>e.Supplier), opt => opt.Ignore())
                .ReverseMap()
                .ForPath(x => x.CatalogId, x => x.Ignore());  
               
        }
    }
}