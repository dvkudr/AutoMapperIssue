using System;
using AutoMapper;
using NUnit.Framework;

namespace AutoMapper_issue
{
    [TestFixture]
    public class Issue2
    {
        class Source
        {
        }

        class Destination
        {
            public string Name;
        }

        class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Source, Destination>()
                    .ForMember(
                        dst => dst.Name,
                        cfg => cfg.MapFrom((src, dst, member, context) => context.Mapper.Map<string>(null)));
            }
        }

        [Test]
        public void MapFromContext()
        {
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();

            var source = new Source();

            Assert.DoesNotThrow(() => mapper.Map<Destination>(source));
        }
    }
}
