using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models
{
    //cevap modelim
    public class Results
    {
       
       private Guid _ResultId = Guid.Empty;
        [Key]
        public Guid ResultId
        {
            get
            {
                if (_ResultId == Guid.Empty)
                {
                    _ResultId = Guid.NewGuid();
                }
                return _ResultId;
            }
            set
            {
                _ResultId = value;
            }
        }
        
        public Guid QuestionId { get; set; }
      
        public Guid OptionId { get; set; }

    }
}
