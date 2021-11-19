using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApplication.Models
{
    //sorular modelimiz
    public partial class Questions
    {
        public Questions()
        {
            ListOption = new HashSet<Options>();
        }
        
     
        private Guid _QuestionId = Guid.Empty;
        [Key]
        public Guid QuestionId
        {
            get
            {
                if (_QuestionId == Guid.Empty)
                {
                    _QuestionId = Guid.NewGuid();
                }
                return _QuestionId;
            }
            set
            {
                _QuestionId = value;
            }
        }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int QuestionId { get; set; }
        [Required]
        public string Name { get; set; }

        public Guid ResultId { get; set; }
        public Guid SurveyId { get; set; }
        public virtual ICollection<Options> ListOption { get; set; }

        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }

    }
}
