using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public Category CategoryGetById(int id)
        {
            return _categoryDal.Get(c=>c.Id == id);
        }

        public void Delete(int categoryId)
        {
            //delete metodu benden Category ister bende ise categoryıd var.
            
            _categoryDal.Delete(new Category { Id=categoryId});
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetList();
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
