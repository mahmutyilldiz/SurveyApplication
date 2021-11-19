using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models
{
    //secenekler modelimiz
    public class Options
    {
        private Guid _OptionId = Guid.Empty;

        [Key]
        public Guid OptionId
        {
            get
            {
                if (_OptionId == Guid.Empty)
                {
                    _OptionId = Guid.NewGuid();
                }
                return _OptionId;
            }
            set
            {
                _OptionId = value;
            }
        }
        [Required]
        public string Name { get; set; }
        public Guid QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Question { get; set; }
    }
}
