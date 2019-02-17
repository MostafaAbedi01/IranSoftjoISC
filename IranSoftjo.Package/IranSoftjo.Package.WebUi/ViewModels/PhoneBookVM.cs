using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PhoneBookVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int PhoneBookID { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "فکس")]
        public string Fax { get; set; }

        [Display(Name = "سایت")]
        public string Site { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "توضیحات")]
        public string Comment { get; set; }

        [Display(Name = "تلفن")]
        public string PhoneNumber { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

           [Display(Name = "گروه عنوان")]
        public string PhoneBookGroupTitle { get; set; }

        public virtual PhoneBookGroup PhoneBookGroups { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PhoneBookGroupID { get; set; }

        public static explicit operator PhoneBook(PhoneBookVM model)
        {
            var pages = new PhoneBook
                        {
                            PhoneBookID = model.PhoneBookID,
                            Email = model.Email,
                            Fax = model.Fax,
                            Site = model.Site,
                            Mobile = model.Mobile,
                            Comment = model.Comment,
                            PhoneNumber = model.PhoneNumber,
                            PhoneBookGroup = model.PhoneBookGroups,
                            PhoneBookGroupID = model.PhoneBookGroupID,
                            Title = model.Title,
                        };
            return pages;
        }

        public static explicit operator PhoneBookVM(PhoneBook model)
        {
                var pages = new PhoneBookVM
                            {
                                PhoneBookID = model.PhoneBookID,
                                Email = model.Email,
                                Fax = model.Fax,
                                Site = model.Site,
                                Mobile = model.Mobile,
                                Comment = model.Comment,
                                PhoneNumber = model.PhoneNumber,
                                PhoneBookGroups = model.PhoneBookGroup,
                                PhoneBookGroupID = (int) model.PhoneBookGroupID,
                                Title = model.Title,
                            };
                if (model.PhoneBookGroup != null)
                {
                    pages.PhoneBookGroupTitle = model.PhoneBookGroup.PhoneBookGroupTitle;
                    pages.PhoneBookGroupID = model.PhoneBookGroup.PhoneBookGroupID;
                }
                return pages;
        }

        public static IEnumerable<PhoneBookVM> ToIEnumerable(IEnumerable<PhoneBook> models)
        {
            IEnumerable<PhoneBookVM> pages = models.Select(model => new PhoneBookVM
                                                                {
                                                                    PhoneBookID = model.PhoneBookID,
                                                                    Email = model.Email,
                                                                    Fax = model.Fax,
                                                                    Site = model.Site,
                                                                    Mobile = model.Mobile,
                                                                    Comment = model.Comment,
                                                                    PhoneBookGroups = model.PhoneBookGroup,
                                                                    PhoneNumber = model.PhoneNumber,
                                                                    Title = model.Title,
                                                                    PhoneBookGroupTitle = model.PhoneBookGroup.PhoneBookGroupTitle,
                                                                });
            return pages;
        }
    }
}