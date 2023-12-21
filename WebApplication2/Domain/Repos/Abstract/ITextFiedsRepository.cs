using WebApplication2.Domain.Entities;

namespace WebApplication2.Domain.Repos.Abstract
{
    public interface ITextFiedsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
