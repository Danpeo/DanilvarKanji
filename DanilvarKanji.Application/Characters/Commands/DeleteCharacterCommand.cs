using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Characters.Commands;

public class DeleteCharacterCommand : IRequest<Result>
{
    public string Id { get; set; }

    public DeleteCharacterCommand(string id)
    {
        Id = id;
    }
}