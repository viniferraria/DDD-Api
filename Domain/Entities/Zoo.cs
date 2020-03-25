using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("ZOO")]
    public class Zoo
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Specie { get; set; }

        public Zoo(int id, string name, string specie)
        {
            this.Id = id;
            this.Name = name;
            this.Specie = specie;
        }

        public Zoo()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
