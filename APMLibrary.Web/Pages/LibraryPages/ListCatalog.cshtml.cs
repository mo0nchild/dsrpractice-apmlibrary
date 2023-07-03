using APMLibrary.Web.Configurations;
using APMLibrary.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace APMLibrary.Web.Pages.LibraryPages
{
    [ValidateAntiForgeryToken]
    public partial class ListCatalogModel : PageModel
    {
        public readonly ListCatalogOptions pageOptions = default!;
        public ListCatalogModel(IOptions<ListCatalogOptions> options) : base() 
        {
            this.pageOptions = options.Value;
        }

        [FromQuery, BindProperty(SupportsGet = true)]
        public string? CategoryGenre { get; set; } = default!;

        [FromQuery, BindProperty(SupportsGet = true)]
        public FilterViewModel FilterModel { get; set; } = new();

        [FromQuery, BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = default!;

        public BookCatalogViewModel Catalog { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            this.Catalog.Items.Add(new () 
            {
                Description = "������ ����� ���������� ���, ��������� ��������, ��������� �������� ���������� ������. ������� ������ � ��������, ��� ������ ��� ��� �������� �������. ������� ������� ��������, ������, ���������� ������������� � ������ ������� �������� �����. ������� ��� �������� �� ��������� ���,��� ������� ��� , ����� ��� ������ ���� � ��� ��� �����, ��� ���������, ������� ���� ������ �� ����� ������� ������.",
                FirstLine = "�������� ����� 1",
                LastLine = "������ ����� 1",
                ForSubscriber = false,
                PageCount = 200,
                Rating = 3.5
            });

            this.Catalog.AllCount = 2000;
            return this.Page();
        }
    }
}
