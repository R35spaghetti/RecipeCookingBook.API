namespace RecipeCookingBookDto.Library.Dto
{
    public record LeftOverToCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public int Amount { get; set; }
    }
}