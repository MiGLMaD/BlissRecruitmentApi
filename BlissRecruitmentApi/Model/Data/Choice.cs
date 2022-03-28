using System.ComponentModel.DataAnnotations.Schema;

namespace bliss.recruitment.api.Model.Data
{
    public class Choice
    {
        public int Id { get; set; }
        [Column("Choice")]
        public string Name { get; set; }
        public int Votes { get; set; }
        public int QuestionId { get; set; }
    }
}
