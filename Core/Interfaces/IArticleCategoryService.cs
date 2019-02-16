using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArticleCategoryService
    {
        Task<IReadOnlyList<Category>> ListCategories();
        Task<Category> GetCategory(int categoryId);
        Task<IReadOnlyList<Category>> ListSubCategories(int parentCategoryId);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int categoryId);
        Task<IReadOnlyList<Article>> ListArticles(int skip, int take,int? categoryId);
        Task<Article> GetArticle(int articleId);
        Task<Article> AddArticle(Article article);
        Task<Article> UpdateArticle(Article article);
        Task<Article> DeleteArticle(int articleId);
        Task<int> CountArticle(int? categoryId);
    }
}
