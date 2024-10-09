using System.Collections.Generic;

namespace Catalog.API.Application.Response;

public record CreateResponse(string Id);
public record GetAllResponse<T>(IEnumerable<T> Results);
public record GetByIdResponse<T>(T Object);
public record GetByFiltersReponse<T>(IEnumerable<T> Results);