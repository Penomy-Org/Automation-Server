namespace ApiTemplate.Presentation.Filters.SetStateBag;

public sealed class StateBag
{
    public Request HttpRequest { get; set; }

    public Response HttpResponse { get; set; }
}
