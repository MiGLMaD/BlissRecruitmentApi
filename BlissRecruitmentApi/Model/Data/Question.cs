using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bliss.recruitment.api.Model.Data
{
    public class Question
    {
        public int Id { get; set; }
        [Column("Question")]
        public string Name { get; set; }
        public string Image_Url { get; set; }
        public string Thumb_Url { get; set; }
        public DateTime Published_At { get; set; }
    }
}
