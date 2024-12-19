using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCategory
    {
        enum enMode { Add, Update }
        enMode _Mode = enMode.Add;

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public clsCategory()
        {
            CategoryId = 0;
            CategoryName = string.Empty;

            _Mode = enMode.Add;
        }

        public clsCategory(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;

            _Mode = enMode.Update;
        }

        static public clsCategory Find(int CategoryId)
        {
            string categoryName = string.Empty;

            if (clsCategoryData.GetCategory(CategoryId, ref categoryName))
            {
                return new clsCategory(CategoryId, categoryName);
            }

            return null;
        }

        static public clsCategory Find(string CategoryName)
        {
            int id = -1;

            if (clsCategoryData.GetCategory(CategoryName, ref id))
            {
                return new clsCategory(id, CategoryName);
            }

            return null;
        }

        static public bool IsExist(int CategoryId)
        {
            return clsCategoryData.IsCategoryExist(CategoryId);
        }

        static public bool Delete(int CategoryId)
        {
            return clsCategoryData.DeleteCategory(CategoryId);
        }

        static public DataTable GetCategories()
        {
            return clsCategoryData.GetAllCategories();
        }

        private bool _Add()
        {
            int categoryId = clsCategoryData.AddNewCategory(CategoryName);

            if (categoryId > 0)
            {
                this.CategoryId = categoryId;
                return true;
            }

            return false;
        }

        private bool _Update()
        {
            return clsCategoryData.UpdateCategoryInfo(CategoryId, CategoryName);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_Add())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();
            }

            return false;
        }
    }

}
