using DanilvarKanji.Shared.Domain.Params;

namespace DanilvarKanji.Shared.Requests.Characters;

public record ListCharactersRequest(PaginationParams? PaginationParams);