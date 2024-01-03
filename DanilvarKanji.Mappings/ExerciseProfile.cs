using AutoMapper;
using DanilvarKanji.Application.Exercises.Commands;
using DanilvarKanji.Domain.Entities.Exercises;
using DanilvarKanji.Shared.Requests.Exercises;

namespace DanilvarKanji.Mappings;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<Exercise, CreateExerciseCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateExerciseCommand, CreateExerciseRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateExerciseRequest, CreateExerciseCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}