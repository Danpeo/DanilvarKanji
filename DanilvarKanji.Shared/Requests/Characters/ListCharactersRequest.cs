using DanilvarKanji.Domain.Shared.Params;

namespace DanilvarKanji.Shared.Requests.Characters;

public record ListCharactersRequest(PaginationParams? PaginationParams);