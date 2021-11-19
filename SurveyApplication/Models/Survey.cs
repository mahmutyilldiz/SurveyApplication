using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models
{
    public class Survey
    {

        public Survey()
        {
            ListQuestion = new HashSet<Questions>();
        }

        //[Key]
       // public int SurveyId { get; set; }
      
        private Guid _SurveyId = Guid.Empty;
        [Key]
        public Guid SurveyId
        {
            get
            {
                if (_SurveyId == Guid.Empty)
                {
                    _SurveyId = Guid.NewGuid();
                }
                return _SurveyId;
            }
            set
            {
                _SurveyId = value;
            }
        }
        public string Link { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<Questions> ListQuestion { get; set; }
    }
}
