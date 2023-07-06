using EntityLayer.Entities;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void Add(Category category);
        void Delete(int categoryId);
        void Update(Category category);
        Category CategoryGetById(int id);

    }
}
