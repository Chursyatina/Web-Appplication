namespace Application.Interfaces
{
    public interface INamedRequestDto : IRequestDto
    {
        public string Name { get; set; }
    }
}
