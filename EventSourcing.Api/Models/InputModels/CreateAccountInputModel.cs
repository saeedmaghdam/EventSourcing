namespace EventSourcing.Api.Models.InputModels
{
    public class CreateAccountInputModel
    {
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
