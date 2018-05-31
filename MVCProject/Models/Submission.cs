using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int DisciplineId { get; set; }
        public double FirstMark { get; set; }
        public string FirstMarkUserId { get; set; }
        public double SecondMark { get; set; }
        public string SecondMarkUserId { get; set; }
        public double ThirdMark { get; set; }
        public string ThirdMarkUserId { get; set; }
        public string FinalMark { get; set; }
        public bool isCheckedNow { get; set; }
    }
}
