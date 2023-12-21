using WebApplication2.Domain.Repos.Abstract;

namespace WebApplication2.Domain
{
    public class DataManager
    {
        public ITextFiedsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFiedsRepository textFieldsRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}
