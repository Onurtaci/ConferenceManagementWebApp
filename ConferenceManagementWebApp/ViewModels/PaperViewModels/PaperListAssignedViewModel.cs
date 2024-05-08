using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.PaperViewModels
{
    public class PaperListAssignedViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string SessionId { get; set; }

        [Required(ErrorMessage = Messages.TitleRequired)]
        [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = Messages.AbstractRequired)]
        [StringLength(500, ErrorMessage = Messages.AbstractMaxLength)]
        public string Abstract { get; set; }

        [Required(ErrorMessage = Messages.KeywordsRequired)]
        [StringLength(50, ErrorMessage = Messages.KeywordsMaxLength)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = Messages.RecommendationRequired)]
        [EnumDataType(typeof(Recommendation), ErrorMessage = Messages.RecommendationRequired)]
        public Recommendation Recommendation { get; set; }
    }
}
