using System;
using AutoMapper;
using NUnit.Framework;

namespace AutoMapper_issue
{
    class FirstName
    {
        public string Name;
    }

    class LastName
    {
        public string Name;
    }

    class FullName
    {
        public string Name;
    }

    class CombinedNames
    {
        public FirstName First;

        public LastName Last;
    }

    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CombinedNames, FullName>()
                .ForMember(dst => dst.Name, cfg => cfg.MapFrom(src => string.Concat(src.First.Name, " ", src.Last.Name)));
        }
    }

    [TestFixture]
    public class MappingTests
    {
        [TestCase("John", "Doe")]
        [TestCase(null, "Doe")]
        [TestCase("John", null)]
        public void StringConcatMapping(string firstName, string lastName)
        {
            var mapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); }).CreateMapper();

            var combinedNames = new CombinedNames
            {
                First = new FirstName { Name = firstName },
                Last = new LastName { Name = lastName }
            };

            var full = mapper.Map<FullName>(combinedNames);

            Assert.That(full, Is.Not.Null);
            Assert.That(full.Name, Is.EqualTo(string.Concat(combinedNames.First.Name, " ", combinedNames.Last.Name)));
        }
    }
}
