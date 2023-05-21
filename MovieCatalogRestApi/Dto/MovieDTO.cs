using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Movie_Catalog_REST_API.Dto
{
    public class MovieDTO : IValidatableObject
    {
        [Required] 
        [MinLength(3,ErrorMessage = "Title can't be shorter than 3 characters."), 
        MaxLength(50,ErrorMessage = "Title can't be longer than 50 characters.") ] 
        [RegularExpression(@"^[a-zA-Z,.!0-9 -]*$", ErrorMessage = "Title can't contain special characters.")]
        public string Title { get; set; } = string.Empty;


        [Required] 
        [DataTypeAttribute(DataType.Date, ErrorMessage = "Must be DateOnly format (YYYY-MM-DD).")]
        public DateTime WorldPremiereDate { get; set; }


        [Required]
        [MaxLength(5, ErrorMessage = "Genre name can't be longer than 5 categories.")]
        public ICollection<string> Genre { get; set; } = new List<string>();


        [Required]
        [MaxLength(5, ErrorMessage = "Directors' list can't be longer than 5.")] 
        public ICollection<string> Director { get; set; } = new List<string>();


        [Required] 
        [Range(1, int.MaxValue, ErrorMessage = "Movie duration can't be shorther than 1 minute.")]
        public int Duration { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (string item in Genre)
            {
                if(!Regex.IsMatch(item, @"^[a-zA-Z ]*$"))
                    yield return new ValidationResult("Genre name can contain only letters.");
                if (item.Length < 3)
                    yield return new ValidationResult("Genre name can't be shorther than 3 chacters.");
            }
            foreach (var item in Director)
            {
                if (!Regex.IsMatch(item, @"^[a-zA-Z0-9 -]*$"))
                    yield return new ValidationResult("Director's name can't contain special characters");
                if (item.Length < 3)
                    yield return new ValidationResult("Director's name can't be shorther than 3 chacters.");

            }
        }
    }
}
