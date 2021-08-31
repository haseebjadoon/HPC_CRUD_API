using System.ComponentModel.DataAnnotations;

namespace HPCAPI
{
    public class Ship
    {
        [Key]
        public int ShipId { get; set; }

        [Required, StringLength(12)]
        [RegularExpression(@"[A-Za-zÀ-ÿ]{4}-\d{4}-[A-Za-zÀ-ÿ]\d", ErrorMessage = "The ship code must be in the format AAAA-1111-A1")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        public double Width { get; set; }

        public override bool Equals(object obj)
        {
            Ship ship = obj as Ship;

            if (ship == null)
                return false;

            return (ship.ShipId.Equals(this.ShipId)
                && ship.Code.Equals(this.Code)
                && ship.Name.Equals(this.Name)
                && ship.Length.Equals(this.Length)
                && ship.Width.Equals(this.Width));
        }
    }
}
