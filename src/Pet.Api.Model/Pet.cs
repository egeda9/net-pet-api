using Pet.Api.Model.Enum;

namespace Pet.Api.Model
{
    public class Pet
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Name { get; set; }

        public SpecieEnum Specie { get; set; }

        public SexEnum Sex { get; set; }

        public double Age { get; set; }

        public string Breed { get; set; }
    }
}