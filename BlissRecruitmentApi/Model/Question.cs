using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace bliss.recruitment.api.Model
{
    public class Question
    {
        public int id { get; set; }
        public string question { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public DateTime published_at { get; set; }

        public List<Choice> choices { get; set; }
    }
}
