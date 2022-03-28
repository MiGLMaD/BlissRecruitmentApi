using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bliss.recruitment.api.Model
{
    public class PostQuestionRequest
    {
        [Required]
        public string question { get; set; }
        [Required]
        public string image_url { get; set; }
        [Required]
        public string thumb_url { get; set; }
        [Required]
        public List<string> choices { get; set; }
    }
}
