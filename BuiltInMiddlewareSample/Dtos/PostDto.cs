using BuiltInMiddlewareSample.Controllers;

using System.ComponentModel.DataAnnotations;

namespace BuiltInMiddlewareSample.Dtos
{
  public class PostDto
  {
    //[Required(ErrorMessageResourceName = "Resources.Controllers.LanguagesController.Required")]
    public string Title { get; set; }

   
    

  }
}
